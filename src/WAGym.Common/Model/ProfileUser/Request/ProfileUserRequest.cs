using WAGym.Common.Model.Profile.Request;

namespace WAGym.Common.Model.ProfileUser.Request
{
    public class ProfileUserRequest
    {
        public long CompanyId { get; set; }
        public long PersonId { get; set; }
        public long UserId { get; set; }
        public List<ProfileRequest> Profiles { get; set; } = new List<ProfileRequest>();
    }
}
