using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class ProfileUserLogTests
    {
        private ProfileUserLog _profileUserLog;

        [TestInitialize]
        public void Setup()
        {
            _profileUserLog = new ProfileUserLog();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.Id = expected;

            Assert.AreEqual(expected, _profileUserLog.Id);
            Assert.IsInstanceOfType(_profileUserLog.Id, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.PersonId = expected;

            Assert.AreEqual(expected, _profileUserLog.PersonId);
            Assert.IsInstanceOfType(_profileUserLog.PersonId, typeof(long));
        }

        [TestMethod]
        public void UserId_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.UserId = expected;

            Assert.AreEqual(expected, _profileUserLog.UserId);
            Assert.IsInstanceOfType(_profileUserLog.UserId, typeof(long));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.ProfileId = expected;

            Assert.AreEqual(expected, _profileUserLog.ProfileId);
            Assert.IsInstanceOfType(_profileUserLog.ProfileId, typeof(long));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.CompanyId = expected;

            Assert.AreEqual(expected, _profileUserLog.CompanyId);
            Assert.IsInstanceOfType(_profileUserLog.CompanyId, typeof(long));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _profileUserLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _profileUserLog.UserUpdateId);
            Assert.IsInstanceOfType(_profileUserLog.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void Operation_StoresCorrectly()
        {
            int expected = (int)OperationEnum.Insert;

            _profileUserLog.Operation = expected;

            Assert.AreEqual(expected, _profileUserLog.Operation);
            Assert.IsInstanceOfType(_profileUserLog.Operation, typeof(int));
        }

        [TestMethod]
        public void OperationNote_StoresCorrectly()
        {
            string expected = "INS";

            _profileUserLog.OperationNote = expected;

            Assert.AreEqual(expected, _profileUserLog.OperationNote);
            Assert.IsInstanceOfType(_profileUserLog.OperationNote, typeof(string));
        }

        [TestMethod]
        public void OperationDate_StoresCorrectly()
        {
            DateTime expected = DateTime.Now;

            _profileUserLog.OperationDate = expected;

            Assert.AreEqual(expected, _profileUserLog.OperationDate);
            Assert.IsInstanceOfType(_profileUserLog.OperationDate, typeof(DateTime));
        }

        [TestMethod]
        public void Profile_StoresProfile()
        {
            _profileUserLog.Profile = new Profile();

            Assert.IsInstanceOfType(_profileUserLog.Profile, typeof(Profile));
        }

        [TestMethod]
        public void User_StoresUser()
        {
            _profileUserLog.User = new User();

            Assert.IsInstanceOfType(_profileUserLog.User, typeof(User));
        }

        [TestMethod]
        public void UserUpdate_StoresUser()
        {
            _profileUserLog.UserUpdate = new User();

            Assert.IsInstanceOfType(_profileUserLog.UserUpdate, typeof(User));
        }
    }
}
