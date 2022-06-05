using Calo.Domain.Base;
using Calo.Domain.Entities.MetabolicRate;
using System.Security.Cryptography;
using System.Text;

namespace Calo.Core.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string? RefreshToken { get; set; }
        public Guid? SelectedDietId { get; set; }
        public Guid? MetabolicRateId { get; set; }

        public IList<MetabolicRate> MetabolicRates { get; set; }

        private const int saltSize = 128 / 8; // 128 bits

        public User() { }

        public User(string login)
        {
            this.UpdateLogin(login);
            this.CreatedDate = DateTime.Now;
        }

        public void UpdateLogin(string login)
        {
            this.Login = login;
            this.ModifiedDate = DateTime.Now;
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            this.RefreshToken = refreshToken;
            this.ModifiedDate = DateTime.Now;
        }

        public void SetPasswordHash(string password, string pepper)
        {
            this.PasswordHash = PreparePasswordHash(password, pepper);
            this.ModifiedDate = DateTime.Now;
        }

        public void GenerateSalt()
        {
            using var generator = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            generator.GetBytes(salt);
            this.Salt = Convert.ToBase64String(salt);
        }

        public bool IsPasswordCorrect(string password, string pepper)
        {
            var passwordHashToCheck = PreparePasswordHash(password, pepper);
            return passwordHashToCheck == this.PasswordHash;
        }

        public bool IsRefreshTokenCorrect(string refreshToken)
        {
            return this.RefreshToken == refreshToken;
        }

        private string PreparePasswordHash(string password, string pepper)
        {
            var passwordFormat = string.Format("{0}{1}", this.Salt, password);
            return HMACSHA512(passwordFormat, pepper);
        }

        private static string HMACSHA512(string input, string secretKey)
        {
            var secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            var inputBytes = Encoding.UTF8.GetBytes(input);

            using var hmac = new HMACSHA512(secretkeyBytes);
            var hashValue = hmac.ComputeHash(inputBytes);
            return CalculateHash(hashValue);
        }

        private static string CalculateHash(byte[] hashedInputBytes)
        {
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }

            return hashedInputStringBuilder.ToString();
        }
    }
}
