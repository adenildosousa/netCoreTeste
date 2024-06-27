using WAGym.Common.Enum;
using WAGym.Common.Helper;

namespace WAGym.Common.Model.User.Request
{
    public class PutUserRequest
    {
        public string? Username { get; private set; }
        public string? Password { get; private set; }
        public StatusEnum Status { get; private set; }

        public PutUserRequest(string? username, string? password, StatusEnum status)
        {
            Username = username;
            Password = password != null ? password.Sha512() : null;
            Status = status;
        }
    }
}
