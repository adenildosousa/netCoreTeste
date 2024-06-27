using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Data.Data;

namespace WAGym.Data.Tests.Data
{
    [TestClass]
    public class FunctionalityProfileLogTests
    {
        private FunctionalityProfileLog _functionalityProfileLog;

        [TestInitialize]
        public void Setup()
        {
            _functionalityProfileLog = new FunctionalityProfileLog();
        }

        [TestMethod]
        public void Id_StoresCorrectly()
        {
            int expected = 1;

            _functionalityProfileLog.Id = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.Id);
            Assert.IsInstanceOfType(_functionalityProfileLog.Id, typeof(int));
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfileLog.CompanyId = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.CompanyId);
            Assert.IsInstanceOfType(_functionalityProfileLog.CompanyId, typeof(long));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfileLog.ProfileId = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.ProfileId);
            Assert.IsInstanceOfType(_functionalityProfileLog.ProfileId, typeof(long));
        }

        [TestMethod]
        public void FunctionalityId_StoresCorrectly()
        {
            int expected = 1;

            _functionalityProfileLog.FunctionalityId = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.FunctionalityId);
            Assert.IsInstanceOfType(_functionalityProfileLog.FunctionalityId, typeof(int));
        }

        [TestMethod]
        public void UserUpdateId_StoresCorrectly()
        {
            long expected = 1;

            _functionalityProfileLog.UserUpdateId = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.UserUpdateId);
            Assert.IsInstanceOfType(_functionalityProfileLog.UserUpdateId, typeof(long));
        }

        [TestMethod]
        public void Operation_StoresCorrectly()
        {
            int expected = 1;

            _functionalityProfileLog.Operation = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.Operation);
            Assert.IsInstanceOfType(_functionalityProfileLog.Operation, typeof(int));
        }

        [TestMethod]
        public void OperationNote_StoresCorrectly()
        {
            string expected = "INS";

            _functionalityProfileLog.OperationNote = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.OperationNote);
            Assert.IsInstanceOfType(_functionalityProfileLog.OperationNote, typeof(string));
        }

        [TestMethod]
        public void OperationDate_StoresCorrectly()
        {
            DateTime expected = DateTime.Now;

            _functionalityProfileLog.OperationDate = expected;

            Assert.AreEqual(expected, _functionalityProfileLog.OperationDate);
            Assert.IsInstanceOfType(_functionalityProfileLog.OperationDate, typeof(DateTime));
        }

        [TestMethod]
        public void Profile_StoresProfile()
        {
            _functionalityProfileLog.Profile = new Profile();

            Assert.IsInstanceOfType(_functionalityProfileLog.Profile, typeof(Profile));
        }

        [TestMethod]
        public void UserUpdate_StoresUser()
        {
            _functionalityProfileLog.UserUpdate = new User();

            Assert.IsInstanceOfType(_functionalityProfileLog.UserUpdate, typeof(User));
        }
    }
}
