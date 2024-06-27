using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Linq.Expressions;
using System.Security.Claims;
using WAGym.Common.Configuration;
using WAGym.Common.Enum;
using WAGym.Common.Exception;
using WAGym.Common.Model.Functionality.Response;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Login.Response;
using WAGym.Common.Model.Profile.Response;
using WAGym.Common.Model.Token;
using WAGym.Common.Resource;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;
using WAGym.Domain.Extension;
using WAGym.Domain.Manager;
using WAGym.Domain.Manager.Interface;

namespace WAGym.Domain.Tests.Manager
{
    [TestClass]
    public class LoginManagerTests
    {
        private LoginManager _loginManager;
        private IUserRepository _userRepository;
        private IFunctionalityProfileRepository _functionalityProfileRepository;
        private IProfileRepository _profileRepository;
        private ITokenManager _tokenManager;
        private ISessionManager _sessionManager;
        private TokenConfiguration _tokenConfiguration;
        private LoginRequest _loginRequest;
        private LoginResponse _loginResponse;
        private User _userResponse;
        private ProfileResponse _profileResponse;
        private string _expectedAccessToken;
        private string _expectedRefreshToken;
        private long _expectedCompanyId;
        private long _expectedPersonId;
        private long _expectedUserId;
        private Token _expectedToken;

        [TestInitialize]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _functionalityProfileRepository = Substitute.For<IFunctionalityProfileRepository>();
            _profileRepository = Substitute.For<IProfileRepository>();
            _tokenManager = Substitute.For<ITokenManager>();
            _sessionManager = Substitute.For<ISessionManager>();
            _tokenConfiguration = Substitute.For<TokenConfiguration>();

            _expectedCompanyId = 1;
            _expectedPersonId = 1;
            _expectedUserId = 1;
            string username = "username";
            string password = "123";
            int profileId = 1;

            _profileResponse = new ProfileResponse()
            {
                Id = 1,
                Name = "Profile 1",
                Functionalities = new List<FunctionalityResponse>()
                {
                    new FunctionalityResponse
                    {
                        Id = 1,
                        Name = "Functionality 1"
                    }
                }
            };
            _expectedToken = new Token(true, DateTime.Now, DateTime.Now.AddMonths(1), _expectedAccessToken, _expectedRefreshToken);
            _loginResponse = new LoginResponse(_expectedCompanyId, _expectedPersonId, _expectedUserId, _expectedToken, _profileResponse);
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            _loginManager = new LoginManager(
                _userRepository,
                _functionalityProfileRepository,
                _profileRepository,
                _tokenManager,
                _sessionManager,
                _tokenConfiguration);

            _loginRequest = new LoginRequest(_expectedCompanyId, username, password, profileId);

            _userResponse = new User
            {
                Username = username,
                PersonId = _expectedPersonId
            };

            _expectedAccessToken = "AccessToken";
            _expectedRefreshToken = "RefreshToken";
        }

        [TestMethod]
        public async Task Authenticate_WithInvalidRequest_ShouldThrowBusinessException()
        {
            _loginRequest = null;

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _loginManager.Authenticate(_loginRequest));
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndNonExistingUser_ShouldThrowBusinessException()
        {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(false);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _loginManager.Authenticate(_loginRequest));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndInvalidPassword_ShouldThrowBusinessException()
        {
            _userResponse = null;
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userResponse);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _loginManager.Authenticate(_loginRequest));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndNonExistingFunctionality_ShouldThrowBusinessException()
        {
            _profileResponse.Functionalities = null;
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userResponse);
            _profileRepository.GetProfileWithFunctionalities(Arg.Any<LoginRequest>(), Arg.Any<long>(), Arg.Any<long>()).Returns(_profileResponse);

            await Assert.ThrowsExceptionAsync<BusinessException>(async () => await _loginManager.Authenticate(_loginRequest));
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).GetProfileWithFunctionalities(Arg.Any<LoginRequest>(), Arg.Any<long>(), Arg.Any<long>());
        }

        [TestMethod]
        public async Task Authenticate_WithValidRequest_AndExistingFunctionality_ShouldReturnLoginResponse_AndGenerateToken()
        {
            _userRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(true);
            _userRepository.GetAsync(Arg.Any<Expression<Func<User, bool>>>()).Returns(_userResponse);
            _profileRepository.GetProfileWithFunctionalities(Arg.Any<LoginRequest>(), Arg.Any<long>(), Arg.Any<long>()).Returns(_profileResponse);
            _tokenManager.GenerateAccessToken(Arg.Any<IEnumerable<Claim>>()).Returns(_expectedAccessToken);
            _tokenManager.GenerateRefreshToken().Returns(_expectedRefreshToken);
            _sessionManager.SetObject(Arg.Any<string>(), Arg.Any<LoginResponse>());

            LoginResponse result = await _loginManager.Authenticate(_loginRequest);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(result.Token.Authenticated);
            Assert.IsNotNull(result.Token.AccessToken);
            Assert.IsNotNull(result.Token.RefreshToken);
            await _userRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _userRepository.Received(1).GetAsync(Arg.Any<Expression<Func<User, bool>>>());
            await _profileRepository.Received(1).GetProfileWithFunctionalities(Arg.Any<LoginRequest>(), Arg.Any<long>(), Arg.Any<long>());
            _tokenManager.Received(1).GenerateAccessToken(Arg.Any<IEnumerable<Claim>>());
            _tokenManager.Received(1).GenerateRefreshToken();
            _sessionManager.Received(1).SetObject(Arg.Any<string>(), Arg.Any<LoginResponse>());
        }

        [TestMethod]
        public void RefreshToken_WithNonExistingSession_ShouldThrowBusinessException()
        {
            _loginResponse = null;
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);
            _loginManager = new LoginManager(_userRepository, _functionalityProfileRepository, _profileRepository, _tokenManager, _sessionManager, _tokenConfiguration);

            Assert.ThrowsException<BusinessException>(() => _loginManager.RefreshToken());
        }

        [TestMethod]
        public void RefreshToken_WithExpiredToken_ShouldThrowBusinessException()
        {
            _loginResponse.Token.Expiration = DateTime.Now.AddMonths(-1);
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            Assert.ThrowsException<BusinessException>(() => _loginManager.RefreshToken());
            _sessionManager.Received(1).GetLoggedUserSessionInfo(Arg.Any<string>());
        }

        [TestMethod]
        public void RefreshToken_WithValidToken_ShouldReturnLoginResponse_AndGenerateNewToken()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            _sessionManager.GetObject<LoginResponse>(Arg.Any<string>()).Returns(_loginResponse);
            _tokenManager.GetPrincipalFromExpiredToken(Arg.Any<string>()).Returns(principal);
            _tokenManager.GenerateAccessToken(Arg.Any<IEnumerable<Claim>>()).Returns(_expectedAccessToken);
            _tokenManager.GenerateRefreshToken().Returns(_expectedRefreshToken);
            _sessionManager.SetObject(Arg.Any<string>(), Arg.Any<LoginResponse>());

            LoginResponse result = _loginManager.RefreshToken();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(result.Token.Authenticated);
            Assert.IsNotNull(result.Token.AccessToken);
            Assert.IsNotNull(result.Token.RefreshToken);
            _tokenManager.Received(1).GetPrincipalFromExpiredToken(Arg.Any<string>());
            _tokenManager.Received(1).GenerateAccessToken(Arg.Any<IEnumerable<Claim>>());
            _tokenManager.Received(1).GenerateRefreshToken();
            _sessionManager.Received(1).SetObject(Arg.Any<string>(), Arg.Any<LoginResponse>());
        }

        [TestMethod]
        public void RevokeToken_WithNonExistingSession_ShouldThrowBusinessException()
        {
            _loginResponse = null;
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);
            _loginManager = new LoginManager(_userRepository, _functionalityProfileRepository, _profileRepository, _tokenManager, _sessionManager, _tokenConfiguration);

            Assert.ThrowsException<BusinessException>(() => _loginManager.RevokeToken());
            _sessionManager.Received(2).GetLoggedUserSessionInfo(Arg.Any<string>());
        }

        [TestMethod]
        public void RevokeToken_WithExpiredToken_ShouldThrowBusinessException()
        {
            _loginResponse.Token.Expiration = DateTime.Now.AddMonths(-1);
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            Assert.ThrowsException<BusinessException>(() => _loginManager.RevokeToken());
            _sessionManager.Received(1).GetLoggedUserSessionInfo(Arg.Any<string>());
        }

        [TestMethod]
        public void RevokeToken_ValidToken_ShouldKillSession()
        {
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);
            _sessionManager.Kill(Arg.Any<string>());

            LoginResponse result = _loginManager.RevokeToken();

            Assert.IsNotNull(result);
            Assert.IsNull(result.Token);
            _sessionManager.Received(1).GetLoggedUserSessionInfo(Arg.Any<string>());
            _sessionManager.Received(1).Kill(Arg.Any<string>());
        }

        [TestMethod]
        public async Task ProfileHasFunctionality_WithNullLoggedUser_ShouldReturnLoginProfileResponse_AndHasAccessIsFalse()
        {
            _loginResponse = null;
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);
            _loginManager = new LoginManager(_userRepository, _functionalityProfileRepository, _profileRepository, _tokenManager, _sessionManager, _tokenConfiguration);

            LoginProfileResponse result = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.CreateUser);

            Assert.IsFalse(result.HasAccess);
            Assert.AreEqual(Resource.SessionExpired, result.Message);
        }

        [TestMethod]
        public async Task ProfileHasFunctionality_WithValidLoggedUser_AndHasNoAccessToFunctionality_ShouldReturnProfileResponse_AndHasAccessIsFalse()
        {
            _profileResponse = new ProfileResponse()
            {
                Id = 1,
                Name = "Profile 1",
                Functionalities = new List<FunctionalityResponse>()
                {
                    new FunctionalityResponse
                    {
                        Id = 2,
                        Name = "Functionality 2"
                    }
                }
            };
            _loginResponse = new LoginResponse(_expectedCompanyId, _expectedPersonId, _expectedUserId, _expectedToken, _profileResponse);
            _sessionManager.GetLoggedUserSessionInfo(Arg.Any<string>()).Returns(_loginResponse);

            _loginManager = new LoginManager(
                _userRepository,
                _functionalityProfileRepository,
                _profileRepository,
                _tokenManager,
                _sessionManager,
                _tokenConfiguration);

            LoginProfileResponse result = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.CreateUser);

            Assert.IsFalse(result.HasAccess);
            Assert.AreEqual(string.Format(Resource.ProfileDoesNotHaveAccessToFunctionality, FunctionalityEnum.CreateUser.GetName()), result.Message);
        }

        [TestMethod]
        public async Task ProfileHasFunctionality_WithValidLoggedUser_AndHasAccessToFunctionality_ShouldReturnProfileResponse_AndHasAccessIsTrue()
        {
            _functionalityProfileRepository.AnyAsync(Arg.Any<Expression<Func<FunctionalityProfile, bool>>>()).Returns(true);

            LoginProfileResponse result = await _loginManager.ProfileHasFunctionality(FunctionalityEnum.CreateUser);

            await _functionalityProfileRepository.Received(1).AnyAsync(Arg.Any<Expression<Func<FunctionalityProfile, bool>>>());
            Assert.IsTrue(result.HasAccess);
            Assert.IsNull(result.Message);
        }
    }
}
