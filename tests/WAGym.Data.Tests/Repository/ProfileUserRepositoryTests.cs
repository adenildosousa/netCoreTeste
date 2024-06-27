using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Data.Repository;
using WAGym.Data.Tests.Data;

namespace WAGym.Data.Tests.Repository
{
    [TestClass]
    public class ProfileUserRepositoryTests
    {
        private AppDbContextTests _context;
        private ProfileUserRepository _profileUserRepository;

        [TestInitialize]
        public void Setup()
        {
            _context = new AppDbContextTests();
            _profileUserRepository = new ProfileUserRepository(_context);
        }
    }
}
