using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Profile.Response;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interfaces;

namespace WAGym.Data.Repository.Interface
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<ProfileResponse?> GetProfileWithFunctionalities(LoginRequest request, long personId, long userId);
    }
}
