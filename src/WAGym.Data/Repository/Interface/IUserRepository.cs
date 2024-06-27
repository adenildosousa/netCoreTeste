using System.Linq.Expressions;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interfaces;

namespace WAGym.Data.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(long personId, string username, long companyId);
        Task<User?> GetUserDetail(long id);
        Task<IEnumerable<User>> GetPaginatedList(PaginationRequest request, Expression<Func<User, bool>> expression);
    }
}
