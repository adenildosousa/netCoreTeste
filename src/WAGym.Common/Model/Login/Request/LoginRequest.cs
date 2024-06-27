using WAGym.Common.Helper;

namespace WAGym.Common.Model.Login.Request
{
    public class LoginRequest
    {
        public long CompanyId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int? ProfileId { get; private set; }

        public LoginRequest(long companyId, string username, string password, int? profileId)
        {
            CompanyId = companyId;
            Username = username;
            Password = password.Sha512();
            ProfileId = profileId;
        }
    }
}
