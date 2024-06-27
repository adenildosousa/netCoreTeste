using WAGym.Common.Model.Functionality.Response;

namespace WAGym.Common.Model.Profile.Response
{
    public class ProfileResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FunctionalityResponse> Functionalities { get; set; }
    }
}
