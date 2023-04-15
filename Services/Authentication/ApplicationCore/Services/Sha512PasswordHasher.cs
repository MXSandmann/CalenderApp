using System.Security.Cryptography;
using System.Text;
using WebUI.Services.Contracts;

namespace WebUI.Services
{
    public class Sha512PasswordHasher : IPasswordHasher
    {
        public (string hashedPassword, string salt) HashPassword(string password)
        {
            // Generate a random salt
            var saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            var salt = Convert.ToBase64String(saltBytes);
            var hashedPassword = HashPasswordWithSalt(password, salt);
            return (hashedPassword, salt);
        }

        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            var actualHashedPassword = HashPasswordWithSalt(password, salt);
            return actualHashedPassword == hashedPassword;
        }

        private static string HashPasswordWithSalt(string password, string salt)
        {
            // Create a SHA512 instance
            using var sha512 = SHA512.Create();

            // Convert the password string to a byte array
            var passwordBytes = Encoding.UTF8.GetBytes(password + salt);

            // Compute the hash value of the password bytes
            var hashBytes = sha512.ComputeHash(passwordBytes);

            // Convert the hash bytes to a hexadecimal string
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
