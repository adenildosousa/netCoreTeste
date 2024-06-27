using Microsoft.EntityFrameworkCore;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;

namespace WAGym.Data.Repository
{
    public class FunctionalityProfileRepository : Repository<FunctionalityProfile>, IFunctionalityProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<FunctionalityProfile> _dbSet;
        public FunctionalityProfileRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<FunctionalityProfile>();
        }
    }
}
