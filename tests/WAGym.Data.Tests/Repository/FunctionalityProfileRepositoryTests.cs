using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Data.Data;
using WAGym.Data.Repository;
using WAGym.Data.Repository.Interface;
using WAGym.Data.Tests.Data;

namespace WAGym.Data.Tests.Repository
{
    [TestClass]
    public class FunctionalityProfileRepositoryTests
    {
        private AppDbContextTests _context;
        private IFunctionalityProfileRepository _functionalityProfileRepository;

        [TestInitialize]
        public void Setup()
        {
            _context = new AppDbContextTests();
            _functionalityProfileRepository = new FunctionalityProfileRepository(_context);
        }
    }
}
