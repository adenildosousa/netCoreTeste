using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class FunctionalityProfileTests
    {
        private FunctionalityProfile _functionalityProfile;

        [TestInitialize]
        public void Setup()
        {
            _functionalityProfile = new FunctionalityProfile();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            int expected = 1;

            _functionalityProfile.Id = expected;

            Assert.AreEqual(expected, _functionalityProfile.Id);
            Assert.IsInstanceOfType(_functionalityProfile.Id, typeof(int));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfile.CompanyId = expected;

            Assert.AreEqual(expected, _functionalityProfile.CompanyId);
            Assert.IsInstanceOfType(_functionalityProfile.CompanyId, typeof(long));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfile.ProfileId = expected;

            Assert.AreEqual(expected, _functionalityProfile.ProfileId);
            Assert.IsInstanceOfType(_functionalityProfile.ProfileId, typeof(long));
        }

        [TestMethod]
        public void FunctionalityId_StoresCorrectly()
        {
            int expected = 1;

            _functionalityProfile.FunctionalityId = expected;

            Assert.AreEqual(expected, _functionalityProfile.FunctionalityId);
            Assert.IsInstanceOfType(_functionalityProfile.FunctionalityId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfile.UserUpdateId = expected;

            Assert.AreEqual(expected, _functionalityProfile.UserUpdateId);
            Assert.IsInstanceOfType(_functionalityProfile.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void Functionality_StoresFunctionality()
        {
            _functionalityProfile.Functionality = new Functionality();

            Assert.IsInstanceOfType(_functionalityProfile.Functionality, typeof(Functionality));
        }

        [TestMethod]
        public void Profile_StoresProfile()
        {
            _functionalityProfile.Profile = new Profile();

            Assert.IsInstanceOfType(_functionalityProfile.Profile, typeof(Profile));
        }

        [TestMethod]
        public void UserUpdate_StoresUser()
        {
            _functionalityProfile.UserUpdate = new User();

            Assert.IsInstanceOfType(_functionalityProfile.UserUpdate, typeof(User));
        }
    }
}
