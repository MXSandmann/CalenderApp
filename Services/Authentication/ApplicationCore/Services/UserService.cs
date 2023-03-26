using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Providers.Contracts;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services.Contracts;
using System.Security.Claims;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<User> CreateUser(string username, string password, string email)
        {
            var newUser = new User
            {
                UserName = username,
                Password = password,
                Email = email,
                Role = Role.User
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
                || (!user.Password.Equals(password)))
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
    }
}
