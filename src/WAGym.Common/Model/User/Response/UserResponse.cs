namespace WAGym.Common.Model.User.Response
{
    public class UserResponse
    {
        public long Id { get; private set; }
        public long PersonId { get; private set; }
        public string Username { get; private set; }
        public int StatusId { get; private set; }
        public string Status { get; private set; }
        public long UserUpdateId { get; private set; }
        public string UserUpdate { get; private set; }
        public long CompanyId { get; private set; }

        public UserResponse()
        {
            
        }

        public UserResponse(long id,
                            long personId,
                            string username,
                            int statusId,
                            string status,
                            long userUpdateId,
                            string userUpdate,
                            long companyId)
        {
            Id = id;
            PersonId = personId;
            Username = username;
            StatusId = statusId;
            Status = status;
            UserUpdateId = userUpdateId;
            UserUpdate = userUpdate;
            CompanyId = companyId;
        }
    }
}
