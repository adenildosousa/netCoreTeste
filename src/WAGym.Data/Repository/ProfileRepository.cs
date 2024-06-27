using Microsoft.EntityFrameworkCore;
using WAGym.Common.Model.Functionality.Response;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Profile.Response;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interface;

namespace WAGym.Data.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Profile> _dbSet;

        public ProfileRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Profile>();
        }

        public async Task<ProfileResponse?> GetProfileWithFunctionalities(LoginRequest request, long personId, long userId)
        {
            return await (from pu in _context.Set<ProfileUser>()
                          join p in _context.Set<Profile>() on pu.ProfileId equals p.Id
                          where pu.CompanyId == request.CompanyId && pu.PersonId == personId && pu.UserId == userId && (!request.ProfileId.HasValue ? pu.Default : pu.ProfileId == request.ProfileId)
                    select new ProfileResponse
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Functionalities = (from fp in _context.Set<FunctionalityProfile>()
                                           join f in _context.Set<Functionality>() on fp.FunctionalityId equals f.Id
                                           where fp.ProfileId == p.Id
                                           select new FunctionalityResponse
                                           {
                                               Id = f.Id,
                                               Name = f.Name
                                           }).ToList()
                    }).FirstOrDefaultAsync();
        }
    }
}
