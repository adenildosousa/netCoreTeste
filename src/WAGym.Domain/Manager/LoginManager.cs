using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using WAGym.Common.Configuration;
using WAGym.Common.Enum;
using WAGym.Common.Exception;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Profile.Response;
using WAGym.Common.Model.Token;
using WAGym.Common.Resource;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.Extension;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Manager
{
    public class LoginManager : Manager, ILoginManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IFunctionalityProfileRepository _functionalityProfileRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITokenManager _tokenManager;
        private readonly ISessionManager _sessionManager;
        private readonly TokenConfiguration _configuration;

        public LoginManager(IUserRepository userRepository,
                            IFunctionalityProfileRepository functionalityProfileRepository,
                            IProfileRepository profileRepository,
                            ITokenManager tokenManager,
                            ISessionManager sessionManager,
                            TokenConfiguration configuration) :
            base(sessionManager)
        {
            _userRepository = userRepository;
            _functionalityProfileRepository = functionalityProfileRepository;
            _profileRepository = profileRepository;
            _tokenManager = tokenManager;
            _sessionManager = sessionManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                throw new BusinessException(Resource.InvalidRequest);

            bool userExists = await _userRepository.AnyAsync(x => x.CompanyId == loginRequest.CompanyId && x.Username == loginRequest.Username);

            if (!userExists)
                throw new BusinessException(Resource.UserNotFound);

            User? user = await _userRepository.GetAsync(x => 
                   x.CompanyId == loginRequest.CompanyId 
                && x.Username.Equals(loginRequest.Username) 
                && x.Password.Equals(loginRequest.Password));

            if (user == null)
                throw new BusinessException(Resource.InvalidPassword);

            ProfileResponse? profileResponse = await _profileRepository.GetProfileWithFunctionalities(loginRequest, user.PersonId, user.Id);

            bool hasFunctionalities = profileResponse != null && profileResponse.Functionalities != null ? profileResponse.Functionalities.Any() : false;

            if (!hasFunctionalities)
                throw new BusinessException(Resource.ProfileWithoutFunctionalities);

            LoginResponse response = new LoginResponse(
                loginRequest.CompanyId,
                user.PersonId,
                user.Id,
                GenerateAndGetAccessToken(user, loginRequest.CompanyId),
                profileResponse);

            _sessionManager.SetObject("LoggedUserInfo", response);

            return response;
        }

        public LoginResponse RefreshToken()
        {
            if (LoggedUser == null || LoggedUser.Token.Expiration <= DateTime.Now)
                throw new BusinessException(Resource.SessionExpired);
            
            ClaimsPrincipal principal = _tokenManager.GetPrincipalFromExpiredToken(LoggedUser.Token.AccessToken);

            LoggedUser.Token.AccessToken = _tokenManager.GenerateAccessToken(principal.Claims);
            LoggedUser.Token.RefreshToken = _tokenManager.GenerateRefreshToken();

            _sessionManager.SetObject("LoggedUserInfo", LoggedUser);

            return LoggedUser;
        }

        public LoginResponse RevokeToken()
        {
            if (LoggedUser == null || LoggedUser.Token.Expiration <= DateTime.Now)
                throw new BusinessException(Resource.SessionExpired);

            _sessionManager.Kill("LoggedUserInfo");
            LoggedUser.Token = null;

            return LoggedUser;
        }

        public async Task<LoginProfileResponse> ProfileHasFunctionality(FunctionalityEnum functionality)
        {
            LoginProfileResponse response = new LoginProfileResponse(true);

            if (LoggedUser == null)
            {
                response = new LoginProfileResponse(false, Resource.SessionExpired);
                return response;
            }

            int[] functionalityIds = LoggedUser.Profile!.Functionalities.Select(x => x.Id).ToArray();
            response = new LoginProfileResponse(await ProfileHasAccess(functionalityIds, functionality));

            if (!response.HasAccess)
                response.SetMessage(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()));

            return response;
        }

        private Token GenerateAndGetAccessToken(User user, long companyId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim("companyId", companyId.ToString()),
                new Claim("personId", user.PersonId.ToString()),
            };

            bool authenticated = true;
            DateTime created = DateTime.Now;
            DateTime expiration = created.AddMinutes(_configuration.Minutes);
            string accessToken = _tokenManager.GenerateAccessToken(claims);
            string refreshToken = _tokenManager.GenerateRefreshToken();

            return new Token(
                authenticated,
                created,
                expiration,
                accessToken,
                refreshToken);
        }
        
        private async Task<bool> ProfileHasAccess(int[] functionalityIds, FunctionalityEnum functionality)
        {
            return await _functionalityProfileRepository.AnyAsync(x =>
                   x.CompanyId == LoggedUser!.CompanyId
                && x.ProfileId == LoggedUser!.Profile.Id
                && x.FunctionalityId == (int)functionality);
        }
    }
}
