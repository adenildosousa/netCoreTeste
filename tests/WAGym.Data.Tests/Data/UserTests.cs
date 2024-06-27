using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class UserTests
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            _user = new User();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _user.Id = expected;

            Assert.AreEqual(expected, _user.Id);
            Assert.IsInstanceOfType(_user.Id, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            long expected = 1;

            _user.PersonId = expected;

            Assert.AreEqual(expected, _user.PersonId);
            Assert.IsInstanceOfType(_user.PersonId, typeof(long));
        }

        [TestMethod]
        public void Username_StoresCorrectly()
        {
            string expected = "Username Test";

            _user.Username = expected;

            Assert.AreEqual(expected, _user.Username);
            Assert.IsInstanceOfType(_user.Username, typeof(string));
        }

        [TestMethod]
        public void Password_StoresCorrectly()
        {
            string expected = "Password Test";

            _user.Password = expected;

            Assert.AreEqual(expected, _user.Password);
            Assert.IsInstanceOfType(_user.Password, typeof(string));
        }

        [TestMethod]
        public void StatusId_StoresCorrectly()
        {
            int expected = (int)StatusEnum.Active;

            _user.StatusId = expected;

            Assert.AreEqual(expected, _user.StatusId);
            Assert.IsInstanceOfType(_user.StatusId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _user.UserUpdateId = expected;

            Assert.AreEqual(expected, _user.UserUpdateId);
            Assert.IsInstanceOfType(_user.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _user.CompanyId = expected;

            Assert.AreEqual(expected, _user.CompanyId);
            Assert.IsInstanceOfType(_user.CompanyId, typeof(long));
        }

        [TestMethod]
        public void CompanyId_IsNull()
        {
            long? expected = null;

            _user.CompanyId = expected;

            Assert.AreEqual(expected, _user.CompanyId);
            Assert.IsNull(_user.CompanyId);
        }


        [TestMethod]
        public void FunctionalityProfiles_StoresCollectionOfFunctionalityProfile()
        {
            _user.FunctionalityProfiles = new Collection<FunctionalityProfile>();

            Assert.IsInstanceOfType(_user.FunctionalityProfiles, typeof(Collection<FunctionalityProfile>));
        }

        [TestMethod]
        public void InverseUserUpdate_StoresCollectionOfUser()
        {
            _user.InverseUserUpdate = new Collection<User>();

            Assert.IsInstanceOfType(_user.InverseUserUpdate, typeof(Collection<User>));
        }

        [TestMethod]
        public void ProfileUsers_StoresCollectionOfProfileUser()
        {
            _user.ProfileUsers = new Collection<ProfileUser>();

            Assert.IsInstanceOfType(_user.ProfileUsers, typeof(Collection<ProfileUser>));
        }

        [TestMethod]
        public void Profiles_StoresCollectionOfProfile()
        {
            _user.Profiles = new Collection<Profile>();

            Assert.IsInstanceOfType(_user.Profiles, typeof(Collection<Profile>));
        }

        [TestMethod]
        public void UserUpdate_StoresCollectionOfUser()
        {
            _user.UserUpdate = new User();

            Assert.IsInstanceOfType(_user.UserUpdate, typeof(User));
        }
    }
}
