using WAGym.Common.Enum;

namespace WAGym.Common.Model.Login.Request
{
    public class LoginProfileRequest
    {
        public long CompanyId { get; private set; }
        public long ProfileId { get; private set; }
        public FunctionalityEnum Functionality { get; private set; }

        public LoginProfileRequest(long companyId, long profileId, FunctionalityEnum functionality)
        {
            CompanyId = companyId;
            ProfileId = profileId;
            Functionality = functionality;
        }
    }
}
