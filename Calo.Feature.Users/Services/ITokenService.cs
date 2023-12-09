using System.Security.Claims;

namespace Calo.Feature.Users.Services;

public interface ITokenService
{
    public string GetToken(IList<Claim> claims, string secretKey, int expiresTime);
    public bool Validate(string token, string secretKey);
    public ClaimsPrincipal GetPrincipalFromToken(string token, string secretKey);
}
