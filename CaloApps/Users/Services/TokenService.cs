using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaloApps.Users.Services
{
    public class TokenService : ITokenService
    {
        public string GetToken(string login, Guid id)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@345")); // TODO move to configuration
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, login),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:44323", // TODO move to configuration
                audience: "https://localhost:44323", // TODO move to configuration
                claims: claims,
                expires: DateTime.Now.AddMinutes(60), // TODO move to configuration
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
