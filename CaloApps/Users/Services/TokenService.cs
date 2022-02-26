using CaloApps.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaloApps.Users.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public string GetToken(string login, Guid id)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.appSettings.Secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, login),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: this.appSettings.JWTSecurity.Issuer,
                audience: this.appSettings.JWTSecurity.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(this.appSettings.JWTSecurity.ExpiredTime),
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
