using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class UserLogTests
    {
        private UserLog _userLog;

        [TestInitialize]
        public void Setup()
        {
            _userLog = new UserLog();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _userLog.Id = expected;

            Assert.AreEqual(expected, _userLog.Id);
            Assert.IsInstanceOfType(_userLog.Id, typeof(long));
        }

        [TestMethod]
        public void Id_IsNull()
        {
            long? expected = null;

            _userLog.Id = expected;

            Assert.AreEqual(expected, _userLog.Id);
            Assert.IsNull(_userLog.Id);
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            long expected = 1;

            _userLog.PersonId = expected;

            Assert.AreEqual(expected, _userLog.PersonId);
            Assert.IsInstanceOfType(_userLog.PersonId, typeof(long));
        }

        [TestMethod]
        public void Username_StoresCorrectly()
        {
            string expected = "Username Test";

            _userLog.Username = expected;

            Assert.AreEqual(expected, _userLog.Username);
            Assert.IsInstanceOfType(_userLog.Username, typeof(string));
        }

        [TestMethod]
        public void Password_StoresCorrectly()
        {
            string expected = "Password Test";

            _userLog.Password = expected;

            Assert.AreEqual(expected, _userLog.Password);
            Assert.IsInstanceOfType(_userLog.Password, typeof(string));
        }

        [TestMethod]
        public void StatusId_StoresCorrectly()
        {
            int expected = (int)StatusEnum.Active;

            _userLog.StatusId = expected;

            Assert.AreEqual(expected, _userLog.StatusId);
            Assert.IsInstanceOfType(_userLog.StatusId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _userLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _userLog.UserUpdateId);
            Assert.IsInstanceOfType(_userLog.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void UserUpdateId_IsNull()
        {
            long? expected = null;

            _userLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _userLog.UserUpdateId);
            Assert.IsNull(_userLog.UserUpdateId);
        }

        [TestMethod]
        public void Operation_StoresCorrectly()
        {
            int expected = (int)OperationEnum.Insert;

            _userLog.Operation = expected;

            Assert.AreEqual(expected, _userLog.Operation);
            Assert.IsInstanceOfType(_userLog.Operation, typeof(int));
        }

        [TestMethod]
        public void OperationNote_StoresCorrectly()
        {
            string expected = "INS";

            _userLog.OperationNote = expected;

            Assert.AreEqual(expected, _userLog.OperationNote);
            Assert.IsInstanceOfType(_userLog.OperationNote, typeof(string));
        }

        [TestMethod]
        public void OperationDate_StoresCorrectly()
        {
            DateTime expected = DateTime.Now;

            _userLog.OperationDate = expected;

            Assert.AreEqual(expected, _userLog.OperationDate);
            Assert.IsInstanceOfType(_userLog.OperationDate, typeof(DateTime));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _userLog.CompanyId = expected;

            Assert.AreEqual(expected, _userLog.CompanyId);
            Assert.IsInstanceOfType(_userLog.CompanyId, typeof(long));
        }

        [TestMethod]
        public void CompanyId_IsNull()
        {
            long? expected = null;

            _userLog.CompanyId = expected;

            Assert.AreEqual(expected, _userLog.CompanyId);
            Assert.IsNull(_userLog.CompanyId);
        }


        [TestMethod]
        public void IdNavigation_StoresUser()
        {
            _userLog.IdNavigation = new User();

            Assert.IsInstanceOfType(_userLog.IdNavigation, typeof(User));
        }

        [TestMethod]
        public void IdNavigation_IsNull()
        {
            _userLog.IdNavigation = null;

            Assert.IsNull(_userLog.IdNavigation);
        }

        [TestMethod]
        public void UserUpdate_StoresUser()
        {
            _userLog.UserUpdate = new User();

            Assert.IsInstanceOfType(_userLog.UserUpdate, typeof(User));
        }

        [TestMethod]
        public void UserUpdate_IsNull()
        {
            _userLog.UserUpdate = null;

            Assert.IsNull(_userLog.UserUpdate);
        }
    }
}
