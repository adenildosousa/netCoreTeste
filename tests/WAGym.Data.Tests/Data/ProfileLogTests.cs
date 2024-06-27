using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class ProfileLogTests
    {
        private ProfileLog _profileLog;

        [TestInitialize]
        public void Setup()
        {
            _profileLog = new ProfileLog();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _profileLog.Id = expected;

            Assert.AreEqual(expected, _profileLog.Id);
            Assert.IsInstanceOfType(_profileLog.Id, typeof(long));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _profileLog.CompanyId = expected;

            Assert.AreEqual(expected, _profileLog.CompanyId);
            Assert.IsInstanceOfType(_profileLog.CompanyId, typeof(long));
        }

        [TestMethod]
        public void Name_StoresCorrectly()
        {
            string expected = "Name Test";

            _profileLog.Name = expected;

            Assert.AreEqual(expected, _profileLog.Name);
            Assert.IsInstanceOfType(_profileLog.Name, typeof(string));
        }

        [TestMethod]
        public void Description_StoresCorrectly()
        {
            string expected = "Description Test";

            _profileLog.Description = expected;

            Assert.AreEqual(expected, _profileLog.Description);
            Assert.IsInstanceOfType(_profileLog.Description, typeof(string));
        }

        [TestMethod]
        public void Description_IsNull()
        {
            string? expected = null;

            _profileLog.Description = expected;

            Assert.AreEqual(expected, _profileLog.Description);
            Assert.IsNull(_profileLog.Description);
        }

        [TestMethod]
        public void StatusId_StoresCorrectly()
        {
            int expected = (int)StatusEnum.Active;

            _profileLog.StatusId = expected;

            Assert.AreEqual(expected, _profileLog.StatusId);
            Assert.IsInstanceOfType(_profileLog.StatusId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _profileLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _profileLog.UserUpdateId);
            Assert.IsInstanceOfType(_profileLog.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void Operation_StoresCorrectly()
        {
            int expected = (int)OperationEnum.Insert;

            _profileLog.Operation = expected;

            Assert.AreEqual(expected, _profileLog.Operation);
            Assert.IsInstanceOfType(_profileLog.Operation, typeof(int));
        }

        [TestMethod]
        public void OperationNote_StoresCorrectly()
        {
            string expected = "INS";

            _profileLog.OperationNote = expected;

            Assert.AreEqual(expected, _profileLog.OperationNote);
            Assert.IsInstanceOfType(_profileLog.OperationNote, typeof(string));
        }

        [TestMethod]
        public void OperationDate_StoresCorrectly()
        {
            DateTime expected = DateTime.Now;

            _profileLog.OperationDate = expected;

            Assert.AreEqual(expected, _profileLog.OperationDate);
            Assert.IsInstanceOfType(_profileLog.OperationDate, typeof(DateTime));
        }

        [TestMethod]
        public void Profile_StoresCollectionOfProfile()
        {
            _profileLog.Profile = new Profile();

            Assert.IsInstanceOfType(_profileLog.Profile, typeof(Profile));
        }

        [TestMethod]
        public void UserUpdate_StoresCollectionOfUser()
        {
            _profileLog.UserUpdate = new User();

            Assert.IsInstanceOfType(_profileLog.UserUpdate, typeof(User));
        }
    }
}
