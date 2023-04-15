using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Providers.Contracts;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;
using System.Security.Claims;
using WebUI.Services.Contracts;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateUser(string username, string password, string email, string role)
        {
            var (hashedPassword, salt) = _passwordHasher.HashPassword(password);
            var newUser = new User
            {
                UserName = username,
                Password = hashedPassword,
                Salt = salt,
                Email = email,
                Role = Enum.Parse<Role>(role)
            };
            var user = await _userRepository.AddNewUser(newUser);
            return user;
        }

        public async Task<string> LoginUser(string username, string password)
        {
            var user = await _userRepository.GetUser(username);                   

            // Check if such user exists
            // or password hashes match
            if (user == null
                || (!_passwordHasher.VerifyPassword(password, user.Password, user.Salt ?? string.Empty)))
                return string.Empty;

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = _jwtProvider.CreateToken(claims);
            return token;
        }

        public async Task<IEnumerable<User>> GetAllInstructors()
        {
            return await _userRepository.GetAllInstructors();
        }
    }
}
