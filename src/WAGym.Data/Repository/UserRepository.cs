using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;

namespace WAGym.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<bool> UserExists(long personId, string username, long companyId)
        {
            return await _dbSet.AnyAsync(x =>
                    ((x.PersonId == personId || x.Username == username)
                 || (x.PersonId == personId && x.Username == username)) 
                 && x.CompanyId == companyId);
        }

        public async Task<User?> GetUserDetail(long id)
        {
            return await _dbSet
                .Include(x => x.UserUpdate)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetPaginatedList(PaginationRequest request, Expression<Func<User, bool>> expression)
        {
            return await _dbSet
                .Where(expression)
                .OrderBy(x => x.Username)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
        }
    }
}