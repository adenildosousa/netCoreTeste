using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Enum;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class FunctionalityLogTests
    {
        private FunctionalityLog _functionalityLog;

        [TestInitialize]
        public void Setup()
        {
            _functionalityLog = new FunctionalityLog();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            long expected = 1;

            _functionalityLog.Id = expected;

            Assert.AreEqual(expected, _functionalityLog.Id);
            Assert.IsInstanceOfType(_functionalityLog.Id, typeof(long));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityLog.ProfileId = expected;

            Assert.AreEqual(expected, _functionalityLog.ProfileId);
            Assert.IsInstanceOfType(_functionalityLog.ProfileId, typeof(long));
        }

        [TestMethod]
        public void FunctionalityId_StoresCorrectly()
        {
            int expected = 1;

            _functionalityLog.FunctionalityId = expected;

            Assert.AreEqual(expected, _functionalityLog.FunctionalityId);
            Assert.IsInstanceOfType(_functionalityLog.FunctionalityId, typeof(int));
        }

        [TestMethod]
        public void Operation_StoresCorrectly()
        {
            int expected = (int)OperationEnum.Insert;

            _functionalityLog.Operation = expected;

            Assert.AreEqual(expected, _functionalityLog.Operation);
            Assert.IsInstanceOfType(_functionalityLog.Operation, typeof(int));
        }

        [TestMethod]
        public void OperationDate_StoresCorrectly()
        {
            DateTime expected = new DateTime(2023, 01, 01);

            _functionalityLog.OperationDate = expected;

            Assert.AreEqual(expected, _functionalityLog.OperationDate);
            Assert.IsInstanceOfType(_functionalityLog.OperationDate, typeof(DateTime));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityLog.CompanyId = expected;

            Assert.AreEqual(expected, _functionalityLog.CompanyId);
            Assert.IsInstanceOfType(_functionalityLog.CompanyId, typeof(long));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _functionalityLog.UserUpdateId);
            Assert.IsInstanceOfType(_functionalityLog.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityLog.PersonId = expected;

            Assert.AreEqual(expected, _functionalityLog.PersonId);
            Assert.IsInstanceOfType(_functionalityLog.PersonId, typeof(long));
        }

        [TestMethod]
        public void Note_StoresCorrectly()
        {
            string expected = "Note Test";

            _functionalityLog.Note = expected;

            Assert.AreEqual(expected, _functionalityLog.Note);
            Assert.IsInstanceOfType(_functionalityLog.Note, typeof(string));
        }

        [TestMethod]
        public void Note_IsNull()
        {
            string? expected = null;

            _functionalityLog.Note = expected;

            Assert.AreEqual(expected, _functionalityLog.Note);
            Assert.IsNull(_functionalityLog.Note);
        }

        [TestMethod]
        public void FunctionalityLogs_StoresFunctionality()
        {
            _functionalityLog.Functionality = new Functionality();

            Assert.IsInstanceOfType(_functionalityLog.Functionality, typeof(Functionality));
        }

        [TestMethod]
        public void FunctionalityLogs_StoresProfile()
        {
            _functionalityLog.Profile = new Profile();

            Assert.IsInstanceOfType(_functionalityLog.Profile, typeof(Profile));
        }

        [TestMethod]
        public void FunctionalityLogs_User()
        {
            _functionalityLog.User = new User();

            Assert.IsInstanceOfType(_functionalityLog.User, typeof(User));
        }
    }
}
