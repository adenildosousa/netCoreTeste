
using WAGym.Common.Helper;

namespace WAGym.Common.Model.User.Request
{
    public class UserRequest
    {
        public long PersonId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public long CompanyId { get; set; }

        public UserRequest(long personId, string username, string password, long companyId)
        {
            PersonId = personId;
            Username = username;
            Password = password.Sha512();
            CompanyId = companyId;
        }
    }
}
