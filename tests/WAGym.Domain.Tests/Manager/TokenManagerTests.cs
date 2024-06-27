using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WAGym.Common.Configuration;
using WAGym.Common.Exception;
using WAGym.Domain.Manager;

namespace WAGym.Domain.Tests.Manager
{
    [TestClass]
    public class TokenManagerTests
    {
        private TokenManager _tokenManager;
        private TokenConfiguration _configuration;
        private IEnumerable<Claim> _claims;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new TokenConfiguration
            {
                Secret = Guid.NewGuid().ToString(),
                Issuer = "Issuer",
                Audience = "Audience",
                Minutes = 10
            };
            _tokenManager = new TokenManager(_configuration);
            _claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, "username"),
                new Claim("companyId", "1"),
                new Claim("personId", "1")
            };
        }

        [TestMethod]
        public void GenerateAccessToken_WithNullClaims_ShouldThrowBusinessException()
        {
            _claims = null;

            Assert.ThrowsException<BusinessException>(() => _tokenManager.GenerateAccessToken(_claims));
        }

        [TestMethod]
        public void GenerateAccessToken_WithNonExistingClaims_ShouldThrowBusinessException()
        {
            _claims = new List<Claim>();

            Assert.ThrowsException<BusinessException>(() => _tokenManager.GenerateAccessToken(_claims));
        }

        [TestMethod]
        public void GenerateAccessToken_WithValidClaims_ShouldReturnAccessToken()
        {
            Assert.IsNotNull(_tokenManager.GenerateAccessToken(_claims));
        }

        [TestMethod]
        public void GenerateRefreshToken_ShouldReturnRefreshToken()
        {
            Assert.IsNotNull(_tokenManager.GenerateRefreshToken());
        }

        [TestMethod]
        public void GetPrincipalFromExpiredToken_WithNonExistingToken_ShouldThrowBusinessException()
        {
            Assert.ThrowsException<BusinessException>(() => _tokenManager.GetPrincipalFromExpiredToken(string.Empty));
        }

        [TestMethod]
        public void GetPrincipalFromExpiredToken_WithValidToken_ShouldReturnClaimsPrincipal()
        {
            string expiredToken = _tokenManager.GenerateAccessToken(_claims);

            ClaimsPrincipal result = _tokenManager.GetPrincipalFromExpiredToken(expiredToken);

            Assert.IsNotNull(result);
        }
    }
}
