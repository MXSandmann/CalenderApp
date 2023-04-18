using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using ApplicationCore.Providers.Contracts;
using ApplicationCore.Repositories.Contracts;
using ApplicationCore.Services;
using ApplicationCore.Services.Contracts;
using Moq;
using System.Security.Claims;
using WebUI.Services;
using WebUI.Services.Contracts;

namespace Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtProvider> _jwtProviderMock;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserService _sut;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtProviderMock = new Mock<IJwtProvider>();
            _passwordHasher = new Sha512PasswordHasher();
            _sut = new UserService(_userRepositoryMock.Object, _jwtProviderMock.Object, _passwordHasher);
        }

        [Fact]
        public async Task CreateUser_Should_Return_NewUser_When_Created_Successfully()
        {
            // Arrange
            var newUser = new User { UserName = "test_username", Password = "test_password", Email = "test@email.com", Role = Role.Instructor };
            _userRepositoryMock.Setup(x => x.AddNewUser(It.IsAny<User>())).ReturnsAsync(newUser);

            // Act
            var result = await _sut.CreateUser(newUser.UserName, newUser.Password, newUser.Email, newUser.Role.ToString());

            // Assert
            Assert.Equal(newUser, result);
        }

        [Fact]
        public async Task LoginUser_Should_Return_EmptyString_When_Credentials_Are_Invalid()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).ReturnsAsync(() => null);            

            // Act
            var result = await _sut.LoginUser("invalid_username", "invalid_password");

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public async Task LoginUser_Should_Return_Token_When_Credentials_Are_Valid()
        {
            // Arrange
            var password = "valid_password";
            var (hashedPassword, salt) = _passwordHasher.HashPassword(password);
            var user = new User { Id = Guid.NewGuid(), UserName = "valid_username", Password = hashedPassword, Email = "valid@email.com", Role = Role.Instructor, Salt = salt };
            _userRepositoryMock.Setup(x => x.GetUser(user.UserName)).ReturnsAsync(user);
            _jwtProviderMock.Setup(x => x.CreateToken(It.IsAny<IEnumerable<Claim>>())).Returns("valid_token");
            
            // Act
            var result = await _sut.LoginUser(user.UserName, password);

            // Assert
            Assert.Equal("valid_token", result);
        }

        [Fact]
        public async Task GetAllInstructors_Should_Return_Instructors()
        {
            // Arrange
            var instructors = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "Instructor1", Role = Role.Instructor },
                new User { Id = Guid.NewGuid(), UserName = "Instructor2", Role = Role.Instructor }
            };
            _userRepositoryMock.Setup(x => x.GetAllInstructors()).ReturnsAsync(instructors);
            
            // Act
            var result = await _sut.GetAllInstructors();

            // Assert
            Assert.Equal(instructors, result.ToList());
        }
    }
}
