using Calo.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Calo.Feature.Users.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public string GetToken(IList<Claim> claims, string secretKey, int expiresTime)
        {
            var secretSymmetricKey = GetSymmetricSecurityKey(secretKey);
            var signinCredentials = GetSigningCredentials(secretSymmetricKey);
            var tokenOptions = new JwtSecurityToken(
                issuer: this.appSettings.JWTSecurity.Issuer,
                audience: this.appSettings.JWTSecurity.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresTime),
                signingCredentials: signinCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public bool Validate(string token, string secretKey)
        {
            var validationParameters = GetTokenValidationParameters(secretKey);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, string secretKey)
        {
            var validationParameters = GetTokenValidationParameters(secretKey, false);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var principal = jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }

        private TokenValidationParameters GetTokenValidationParameters(string secretKey, bool shouldValidateLifetime = true)
        {
            var secretSymmetricKey = GetSymmetricSecurityKey(secretKey);
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = shouldValidateLifetime,
                IssuerSigningKey = secretSymmetricKey,
                ValidIssuer = this.appSettings.JWTSecurity.Issuer,
                ValidAudience = this.appSettings.JWTSecurity.Audience,
                ClockSkew = TimeSpan.Zero
            };
        }

        private static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey) => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        private static SigningCredentials GetSigningCredentials(SymmetricSecurityKey secretSymmetricKey) => 
            new SigningCredentials(secretSymmetricKey, SecurityAlgorithms.HmacSha512);

    }
}
