using System.Security.Cryptography;
using System.Text;

namespace CaloApps.Users.Services
{
    public class PasswordService : IPasswordService
    {
        private const string pepper = "zdRpf^%f65V(0"; // TODO move to settings
        private const int saltSize = 128 / 8; // 128 bits

        public string PreparePasswordHash(string password, string salt)
        {
            var passwordHash = string.Format("{0}{1}", salt, password);

            return HMACSHA512(passwordHash, pepper);
        }

        public string GenerateSalt()
        {
            using var generator = RandomNumberGenerator.Create();
            var salt = new byte[saltSize];
            generator.GetBytes(salt);
            return Convert.ToBase64String(salt);
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
