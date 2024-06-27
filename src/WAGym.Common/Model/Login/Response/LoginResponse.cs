using WAGym.Common.Model.Profile.Response;

namespace WAGym.Common.Model.Login.Response
{
    public class LoginResponse 
    {
        public long CompanyId { get; private set; }
        public long PersonId { get; private set; }
        public long UserId { get; private set; }
        public Token.Token Token { get; set; }
        public ProfileResponse? Profile { get; private set; }

        public LoginResponse(long companyId,
                             long personId,
                             long userId,
                             Token.Token token,
                             ProfileResponse? profile)
        {
            CompanyId = companyId;
            PersonId = personId;
            UserId = userId;
            Token = token;
            Profile = profile;
        }
    }
}
