using Microsoft.EntityFrameworkCore;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;

namespace WAGym.Data.Repository
{
    public class ProfileUserRepository : Repository<ProfileUser>, IProfileUserRepository
    {
        private readonly AppDbContext _context;
        private DbSet<ProfileUser> _dbSet;

        public ProfileUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<ProfileUser>();
        }
    }
}
