using WebUI.Services.Contracts;

namespace WebUI.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            // Hash the password using bcrypt with a cost factor of 10
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);

            // Return the hashed password as a string
            return hashedPassword;
        }
    }
}
