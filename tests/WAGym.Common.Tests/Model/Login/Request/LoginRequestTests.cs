using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Helper;
using WAGym.Common.Model.Login.Request;

namespace WAGym.Common.Tests.Model.Login.Request
{
    [TestClass]
    public class LoginRequestTests
    {
        private LoginRequest _request;
        private long _expectedCompanyId;
        private string _expectedUsername;
        private string _expectedPassword;
        private int? _expectedProfileId;

        [TestInitialize]
        public void Setup()
        {
            _expectedCompanyId = 1;
            _expectedUsername = "username";
            _expectedPassword = "123";
            _expectedProfileId = 1;

            _request = new LoginRequest(_expectedCompanyId, _expectedUsername, _expectedPassword, _expectedProfileId);
        }

        [TestMethod]
        public void CompanyId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedCompanyId, _request.CompanyId);
            Assert.IsInstanceOfType(_request.CompanyId, typeof(long));
        }

        [TestMethod]
        public void Username_StoresCorrectly()
        {
            Assert.AreEqual(_expectedUsername, _request.Username);
            Assert.IsInstanceOfType(_request.Username, typeof(string));
        }

        [TestMethod]
        public void Password_StoresCorrectly()
        {
            Assert.AreEqual(_expectedPassword.Sha512(), _request.Password);
            Assert.IsInstanceOfType(_request.Password, typeof(string));
        }

        [TestMethod]
        public void ProfileId_StoresCorrectly()
        {
            Assert.AreEqual(_expectedProfileId, _request.ProfileId);
            Assert.IsInstanceOfType(_request.ProfileId, typeof(int));
        }

        [TestMethod]
        public void ProfileId_IsNull()
        {
            _expectedProfileId = null;
            _request = new LoginRequest(_expectedCompanyId, _expectedUsername, _expectedPassword, _expectedProfileId);

            Assert.AreEqual(_expectedProfileId, _request.ProfileId);
            Assert.IsNull(_request.ProfileId);
        }
    }
}
