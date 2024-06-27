using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WAGym.Common.Tests.Model.Token
{
    [TestClass]
    public class TokenTests
    {
        private Common.Model.Token.Token _token;
        private bool _expectedAuthenticated;
        private DateTime _expectedDate;
        private string _expectedAccessToken;
        private string _expectedRefreshToken;

        [TestInitialize]
        public void Setup()
        {
            _expectedAuthenticated = true;
            _expectedDate = DateTime.Now;
            _expectedAccessToken = "AccessToken";
            _expectedRefreshToken = "RefreshToken";

            _token = new Common.Model.Token.Token(_expectedAuthenticated, _expectedDate, _expectedDate, _expectedAccessToken, _expectedRefreshToken);
        }

        [TestMethod]
        public void Authenticated_StoresCorrectly()
        {
            Assert.AreEqual(_expectedAuthenticated, _token.Authenticated);
            Assert.IsInstanceOfType(_token.Authenticated, typeof(bool));
        }

        [TestMethod]
        public void Created_StoresCorrectly()
        {
            Assert.AreEqual(_expectedDate, _token.Created);
            Assert.IsInstanceOfType(_token.Created, typeof(DateTime));
        }

        [TestMethod]
        public void Expiration_StoresCorrectly()
        {
            Assert.AreEqual(_expectedDate, _token.Expiration);
            Assert.IsInstanceOfType(_token.Expiration, typeof(DateTime));
        }

        [TestMethod]
        public void AccessToken_StoresCorrectly()
        {
            Assert.AreEqual(_expectedAccessToken, _token.AccessToken);
            Assert.IsInstanceOfType(_token.AccessToken, typeof(string));
        }

        [TestMethod]
        public void RefreshToken_StoresCorrectly()
        {
            Assert.AreEqual(_expectedRefreshToken, _token.RefreshToken);
            Assert.IsInstanceOfType(_token.RefreshToken, typeof(string));
        }
    }
}
