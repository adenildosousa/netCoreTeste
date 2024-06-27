using WAGym.Common.Enum;
using WAGym.Common.Model.Base.Response;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Common.Model.Pagination.Response;
using WAGym.Common.Model.User.Request;
using WAGym.Common.Model.User.Response;

namespace WAGym.Domain.Manager.Interface
{
    public interface IUserManager : IManager
    {
        Task<BaseResponse> CreateUser(UserRequest request);
        Task<BaseResponse> EditUser(long companyId, long personId, PutUserRequest request);
        Task<UserResponse> DetailUser(long id);
        Task<PaginationResponse<UserResponse>> GetList(PaginationRequest request);
        Task<BaseResponse> DeleteUser(long id, StatusEnum status);
    }
}