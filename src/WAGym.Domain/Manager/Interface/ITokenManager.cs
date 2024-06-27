using System.Security.Claims;

namespace WAGym.Domain.Manager.Interface
{
    public interface ITokenManager : IManager
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
