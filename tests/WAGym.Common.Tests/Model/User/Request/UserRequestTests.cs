using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Helper;
using WAGym.Common.Model.User.Request;

namespace WAGym.Common.Tests.Model.User.Request
{
    [TestClass]
    public class UserRequestTests
    {
        private UserRequest _userRequest;
        private long _expectedPersonId;
        private string _expectedUsername;
        private string _expectedPassword;
        private long _expectedCompanyId;

        [TestInitialize]
        public void Setup()
        {
            _expectedPersonId = 1;
            _expectedUsername = "username";
            _expectedPassword = "123";
            _expectedCompanyId = 1;

            _userRequest = new UserRequest(_expectedPersonId, _expectedUsername, _expectedPassword, _expectedCompanyId);
        }

        [TestMethod]
        public void PersonId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPersonId, _userRequest.PersonId);
            Assert.IsInstanceOfType(_userRequest.PersonId, typeof(long));
        }

        [TestMethod]
        public void Username_StoresCorrectly()
        {
            Assert.AreEqual(_expectedUsername, _userRequest.Username);
            Assert.IsInstanceOfType(_userRequest.Username, typeof(string));
        }

        [TestMethod]
        public void Password_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPassword.Sha512(), _userRequest.Password);
            Assert.IsInstanceOfType(_userRequest.Username, typeof(string));
        }
    }
}
