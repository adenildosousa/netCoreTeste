using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Common.Model.Pagination.Request;
using WAGym.Data.Data;
using WAGym.Data.Repository;
using WAGym.Data.Repository.Interface;
using WAGym.Data.Tests.Data;

namespace WAGym.Data.Tests.Repository
{
    [TestClass]
    public class UserRepositoryTests
    {
        private AppDbContextTests _context;
        private IUserRepository _userRepository;
        private PaginationRequest _paginationRequest;
        private long _expectedId;
        private long _expectedPersonId;
        private string _expectedUsername;
        private long _expectedCompanyId;

        [TestInitialize]
        public async Task Setup()
        {
            _context = new AppDbContextTests();
            _userRepository = new UserRepository(_context);

            await MockSetup();
        }

        [TestMethod]
        public async Task UserExists_WhenUserExists_ShouldReturnTrue()
        {
            Assert.IsTrue(await _userRepository.UserExists(_expectedPersonId, _expectedUsername, _expectedCompanyId));
        }

        [TestMethod]
        public async Task UserExists_WhenPersonIdDoesNotExists_ShouldReturnFalse()
        {
            Assert.IsFalse(await _userRepository.UserExists(10, "username10", _expectedCompanyId));
        }

        [TestMethod]
        public async Task GetUserDetail_WithExistingId_ShouldReturnUser()
        {
            _expectedId = 1;

            User? user = await _userRepository.GetUserDetail(_expectedId);

            Assert.IsNotNull(user);
            Assert.AreEqual(_expectedId, user.Id);

        }

        [TestMethod]
        public async Task GetUserDetail_WithNonExistingId_ShouldReturnNull()
        {
            _expectedId = 10;

            User? user = await _userRepository.GetUserDetail(_expectedId);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task GetPaginatedList_WithValidRequest_ShouldReturnUser()
        {
            IEnumerable<User> result = await _userRepository.GetPaginatedList(_paginationRequest, x => x.CompanyId == _paginationRequest.CompanyId);

            Assert.IsNotNull(result);
            Assert.AreEqual(_paginationRequest.PageSize, result.Count());
        }

        [TestMethod]
        public async Task GetPaginatedList_WithInvalidRequest_ShouldReturnNull()
        {
            _paginationRequest.CompanyId = 0;

            IEnumerable<User> result = await _userRepository.GetPaginatedList(_paginationRequest, x => x.CompanyId == _paginationRequest.CompanyId);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        private async Task MockSetup()
        {
            _expectedId = 1;
            _expectedPersonId = 1;
            _expectedUsername = "username";
            _expectedCompanyId = 1;

            _paginationRequest = new PaginationRequest
            {
                CompanyId = 1,
                PageNumber = 1,
                PageSize = 1
            };

            List<User> users = new List<User>()
            {
                new User
                {
                    CompanyId = 1,
                    PersonId = 1,
                    Username = "username",
                    Password = "123",
                    StatusId = (int)StatusEnum.Active
                },
                new User
                {
                    CompanyId = 1,
                    PersonId = 2,
                    Username = "username2",
                    Password = "123",
                    StatusId = (int)StatusEnum.Active
                }
            };
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
        }
    }
}