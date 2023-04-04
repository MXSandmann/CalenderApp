using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;
using WebUI.Models.Dtos;

namespace Tests.Controllers
{
    public class AccountControllerTest
    {
        private readonly Mock<ILogger<AccountController>> _loggerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AccountController _sut;

        public AccountControllerTest()
        {
            _loggerMock = new Mock<ILogger<AccountController>>();
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _sut = new AccountController(_loggerMock.Object, _userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Login_Should_Return_Unauthorized_When_Invalid_Credentials()
        {
            // Arrange
            _userServiceMock.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => null!);

            // Act
            var result = await _sut.Login("invalid_username", "invalid_password");

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async Task Login_Should_Return_Ok_With_Token_When_Credentials_Are_Valid()
        {
            // Arrange
            _userServiceMock.Setup(x => x.LoginUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("valid_token");
            
            // Act
            var result = await _sut.Login("valid_username", "valid_password");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal("valid_token", ((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_When_Failed_To_Create_User()
        {
            // Arrange
            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => null!);            
            var userRegistrationDto = new UserRegistrationDto { UserName = "test_username", Password = "test_password", Email = "test@email.com", Role = "test_role" };

            // Act
            var result = await _sut.Register(userRegistrationDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Register_Should_Return_Ok_When_User_Created_Successfully()
        {
            // Arrange
            _userServiceMock.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new User { Id = Guid.NewGuid() });            
            var userRegistrationDto = new UserRegistrationDto { UserName = "test_username", Password = "test_password", Email = "test@email.com", Role = "test_role" };

            // Act
            var result = await _sut.Register(userRegistrationDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Instructors_Should_Return_Ok_With_Mapped_InstructorDtos()
        {
            // Arrange
            var instructors = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "Instructor1" },
                new User { Id = Guid.NewGuid(), UserName = "Instructor2" }
            };
            _userServiceMock.Setup(x => x.GetAllInstructors()).ReturnsAsync(instructors);

            var instructorDtos = new List<GetInstructorDto>
            {
                new GetInstructorDto { Id = instructors[0].Id, Name = instructors[0].UserName },
                new GetInstructorDto { Id = instructors[1].Id, Name = instructors[1].UserName }
            };
            _mapperMock.Setup(x => x.Map<IEnumerable<GetInstructorDto>>(instructors)).Returns(instructorDtos);
                       
            // Act
            var result = await _sut.Instructors();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var resultValue = ((OkObjectResult)result).Value as IEnumerable<GetInstructorDto>;
            Assert.Equal(instructorDtos, resultValue!);
        }
    }
}
