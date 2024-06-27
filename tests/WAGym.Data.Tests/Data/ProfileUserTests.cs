using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class ProfileUserTests
    {
        private ProfileUser _profileUser;

        [TestInitialize]
        public void Setup()
        {
            _profileUser = new ProfileUser();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.Id = expected;

            Assert.AreEqual(expected, _profileUser.Id);
            Assert.IsInstanceOfType(_profileUser.Id, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.PersonId = expected;

            Assert.AreEqual(expected, _profileUser.PersonId);
            Assert.IsInstanceOfType(_profileUser.PersonId, typeof(long));
        }

        [TestMethod]
        public void UserId_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.UserId = expected;

            Assert.AreEqual(expected, _profileUser.UserId);
            Assert.IsInstanceOfType(_profileUser.UserId, typeof(long));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.ProfileId = expected;

            Assert.AreEqual(expected, _profileUser.ProfileId);
            Assert.IsInstanceOfType(_profileUser.ProfileId, typeof(long));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.CompanyId = expected;

            Assert.AreEqual(expected, _profileUser.CompanyId);
            Assert.IsInstanceOfType(_profileUser.CompanyId, typeof(long));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _profileUser.UserUpdateId = expected;

            Assert.AreEqual(expected, _profileUser.UserUpdateId);
            Assert.IsInstanceOfType(_profileUser.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void Default_StoresCorrectly()
        {
            bool expected = true;

            _profileUser.Default = expected;

            Assert.AreEqual(expected, _profileUser.Default);
            Assert.IsInstanceOfType(_profileUser.Default, typeof(bool));
        }

        [TestMethod]
        public void Profile_StoresProfile()
        {
            _profileUser.Profile = new Profile();

            Assert.IsInstanceOfType(_profileUser.Profile, typeof(Profile));
        }

        [TestMethod]
        public void User_StoresUser()
        {
            _profileUser.User = new User();

            Assert.IsInstanceOfType(_profileUser.User, typeof(User));
        }
    }
}
