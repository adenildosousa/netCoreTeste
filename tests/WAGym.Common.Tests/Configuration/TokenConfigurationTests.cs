using Microsoft.VisualStudio.TestTools.UnitTesting;
using WAGym.Common.Configuration;

namespace WAGym.Common.Tests.Configuration
{
    [TestClass]
    public class TokenConfigurationTests
    {
        private TokenConfiguration _configuration;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new TokenConfiguration();
        }

        [TestMethod]
        public void Audience_StoresCorrectly()
        {
            string expected = "Audience Test";

            _configuration.Audience = expected;

            Assert.AreEqual(expected, _configuration.Audience);
            Assert.IsInstanceOfType(_configuration.Audience, typeof(string));
        }

        [TestMethod]
        public void Audience_IsNull()
        {
            string? expected = null;

            _configuration.Audience = expected;

            Assert.AreEqual(expected, _configuration.Audience);
            Assert.IsNull(_configuration.Audience);
        }

        [TestMethod]
        public void Issuer_StoresCorrectly()
        {
            string expected = "Issuer Test";

            _configuration.Issuer = expected;

            Assert.AreEqual(expected, _configuration.Issuer);
            Assert.IsInstanceOfType(_configuration.Issuer, typeof(string));
        }

        [TestMethod]
        public void Issuer_IsNull()
        {
            string? expected = null;

            _configuration.Issuer = expected;

            Assert.AreEqual(expected, _configuration.Issuer);
            Assert.IsNull(_configuration.Issuer);
        }

        [TestMethod]
        public void Secret_StoresCorrectly()
        {
            string expected = "Secret Test";

            _configuration.Secret = expected;

            Assert.AreEqual(expected, _configuration.Secret);
            Assert.IsInstanceOfType(_configuration.Secret, typeof(string));
        }

        [TestMethod]
        public void Secret_IsNull()
        {
            string? expected = null;

            _configuration.Secret = expected;

            Assert.AreEqual(expected, _configuration.Secret);
            Assert.IsNull(_configuration.Secret);
        }

        [TestMethod]
        public void Minutes_StoresCorrectly()
        {
            int expected = 1;

            _configuration.Minutes = expected;

            Assert.AreEqual(expected, _configuration.Minutes);
            Assert.IsInstanceOfType(_configuration.Minutes, typeof(int));
        }
    }
}
