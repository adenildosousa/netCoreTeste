using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class ProfileTests
    {
        private Profile _profile;

        [TestInitialize]
        public void Setup()
        {
            _profile = new Profile();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _profile.Id = expected;

            Assert.AreEqual(expected, _profile.Id);
            Assert.IsInstanceOfType(_profile.Id, typeof(long));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _profile.CompanyId = expected;

            Assert.AreEqual(expected, _profile.CompanyId);
            Assert.IsInstanceOfType(_profile.CompanyId, typeof(long));
        }
        
        [TestMethod]
        public void Name_StoresCorrectly()
        {
            string expected = "Name Test";

            _profile.Name = expected;

            Assert.AreEqual(expected, _profile.Name);
            Assert.IsInstanceOfType(_profile.Name, typeof(string));
        }

        [TestMethod]
        public void Description_StoresCorrectly()
        {
            string expected = "Description Test";

            _profile.Description = expected;

            Assert.AreEqual(expected, _profile.Description);
            Assert.IsInstanceOfType(_profile.Description, typeof(string));
        }

        [TestMethod]
        public void Description_IsNull()
        {
            string? expected = null;

            _profile.Description = expected;

            Assert.AreEqual(expected, _profile.Description);
            Assert.IsNull(_profile.Description);
        }

        [TestMethod]
        public void StatusId_StoresCorrectly()
        {
            int expected = (int)StatusEnum.Active;

            _profile.StatusId = expected;

            Assert.AreEqual(expected, _profile.StatusId);
            Assert.IsInstanceOfType(_profile.StatusId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _profile.UserUpdateId = expected;

            Assert.AreEqual(expected, _profile.UserUpdateId);
            Assert.IsInstanceOfType(_profile.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void FunctionalityProfiles_StoresCollectionOfFunctionalityProfile()
        {
            _profile.FunctionalityProfiles = new Collection<FunctionalityProfile>();

            Assert.IsInstanceOfType(_profile.FunctionalityProfiles, typeof(Collection<FunctionalityProfile>));
        }

        [TestMethod]
        public void ProfileUsers_StoresCollectionOfProfileUser()
        {
            _profile.ProfileUsers = new Collection<ProfileUser>();

            Assert.IsInstanceOfType(_profile.ProfileUsers, typeof(Collection<ProfileUser>));
        }

        [TestMethod]
        public void UserUpdate_StoresUser()
        {
            _profile.UserUpdate = new User();

            Assert.IsInstanceOfType(_profile.UserUpdate, typeof(User));
        }
    }
}
