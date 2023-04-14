using System.Security.Cryptography;
using System.Text;
using WebUI.Services.Contracts;

namespace WebUI.Services
{
    public class Sha512PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Create a SHA512 instance
            using var sha512 = SHA512.Create();

            // Convert the password string to a byte array
            var passwordBytes = Encoding.UTF8.GetBytes(password);

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
