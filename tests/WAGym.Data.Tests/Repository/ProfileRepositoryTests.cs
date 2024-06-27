using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Common.Model.Login.Request;
using WAGym.Common.Model.Profile.Response;
using WAGym.Data.Data;
using WAGym.Data.Repository;
using WAGym.Data.Repository.Interface;
using WAGym.Data.Tests.Data;

namespace WAGym.Data.Tests.Repository
{
    [TestClass]
    public class ProfileRepositoryTests
    {
        private AppDbContextTests _context;
        private IProfileRepository _profileRepository;
        private LoginRequest _loginRequest;

        [TestInitialize]
        public async Task Setup()
        {
            _context = new AppDbContextTests();
            _profileRepository = new ProfileRepository(_context);
            _loginRequest = new LoginRequest(1, "username", "123", 1);

            await MockSetup();
        }

        [TestMethod]
        public async Task GetProfileWithFunctionalities_ShouldReturnProfileAndFunctionalities()
        {
            ProfileResponse? result = await _profileRepository.GetProfileWithFunctionalities(_loginRequest, 1, 1);
        
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Functionalities.Any());
        }

        private async Task MockSetup()
        {
            User user = new User
            {
                PersonId = 1,
                Username = "username",
                Password = "123",
                StatusId = (int)StatusEnum.Active
            };
            _context.Users.Add(user);

            Profile profile = new Profile
            {
                CompanyId = 1,
                Name = "Profile 1",
                StatusId = (int)StatusEnum.Active,
                UserUpdate = user
            };
            _context.Profiles.Add(profile);

            ProfileUser profileUser = new ProfileUser
            {
                PersonId = 1,
                User = user,
                Profile = profile,
                CompanyId = 1
            };
            _context.ProfileUsers.Add(profileUser);

            Functionality functionality = new Functionality
            {
                Name = "Functionality 1"
            };
            _context.Functionalities.Add(functionality);

            FunctionalityProfile functionalityProfile = new FunctionalityProfile
            {
                CompanyId = 1,
                Profile = profile,
                Functionality = functionality,
                UserUpdate = user
            };
            _context.FunctionalityProfiles.Add(functionalityProfile);

            await _context.SaveChangesAsync();
        }
    }
}
