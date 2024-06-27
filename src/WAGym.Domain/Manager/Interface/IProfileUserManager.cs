using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.ProfileUser.Request;

namespace WAGym.Domain.Manager.Interface
{
    public interface IProfileUserManager : IManager
    {
        Task<BaseResponse> Create(ProfileUserRequest request);
    }
}
