using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shouldly;
using System.Net;
using System.Security.Claims;
using WebUI.Clients;
using WebUI.Clients.Contracts;
using WebUI.Jwt.Contracts;
using WebUI.Models.Dtos;
using WebUI.Services;
using WebUI.Services.Contracts;

namespace Tests.Clients
{
    public class AuthenticationClientTest
    {
        private readonly IAuthenticationClient _sut;
        private readonly MockHttpMessageHandler _mockHttpHandler;
        private readonly Mock<IJwtValidator> _mockJwtValidator;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationClientTest()
        {
            _mockHttpHandler = new MockHttpMessageHandler();
            var mockClient = new HttpClient(_mockHttpHandler)
            {
                BaseAddress = new Uri("http://random")
            };
            _mockJwtValidator = new();
            _passwordHasher = new Sha512PasswordHasher();            
            _sut = new AuthenticationClient(mockClient, _mockJwtValidator.Object, _passwordHasher);
        }

        [Fact]        
        public async Task GetAllInstructors_ShouldReturnInstructors_WhenAllOk()
        {
            // Arrange
            var count = 4;
            _mockHttpHandler.When(HttpMethod.Get, "/Instructors")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(TestData.GetInstructorDtos(count))));

            // Act
            var results = await _sut.GetAllInstructors();

            // Assert
            var requestResult = Assert.IsAssignableFrom<IEnumerable<GetInstructorDto>>(results);            
            requestResult.Count().ShouldBe(count);
        }

        [Fact]
        public async Task GetAllInstructors_ShouldReturnEmptyInstructors_WhenNoInstructors()
        {
            // Arrange
            var count = 0;
            _mockHttpHandler.When(HttpMethod.Get, "/Instructors")
                .Respond(HttpStatusCode.OK, new StringContent(JsonConvert.SerializeObject(TestData.GetInstructorDtos(count))));

            // Act
            var results = await _sut.GetAllInstructors();

            // Assert
            var requestResult = Assert.IsAssignableFrom<IEnumerable<GetInstructorDto>>(results);
            requestResult.ShouldBeEmpty();            
        }

        [Fact]
        public async Task LoginUser_ShouldReturnClaims_WhenAllOk()
        {
            // Arrange
            var userDto = TestData.GetAutofakedClass<UserDto>();

            _mockHttpHandler.When(HttpMethod.Get, "/Login")
                .Respond(HttpStatusCode.OK, new StringContent("random"));
            _mockJwtValidator.Setup(x => x.ValidateToken(It.IsAny<string>()))
                .Returns(new ClaimsPrincipal());

            // Act
            var result = await _sut.LoginUser(userDto);

            // Assert
            var claims = result.ShouldBeOfType<ClaimsPrincipal>();
            claims.ShouldNotBeNull();
        }

        [Fact]
        public async Task LoginUser_ShouldThrow_WhenResponseIsNotOk()
        {
            // Arrange
            var userDto = TestData.GetAutofakedClass<UserDto>();

            _mockHttpHandler.When(HttpMethod.Get, "/Login")
                .Respond(HttpStatusCode.BadRequest, new StringContent("random"));            

            // Act
            var func = async () => await _sut.LoginUser(userDto);

            // Assert
            await func.ShouldThrowAsync<BadHttpRequestException>();
        }

        [Fact]
        public async Task RegisterNewUser_ShouldNotThrow_WhenAllOk()
        {
            // Arrange
            var dto = TestData.GetAutofakedClass<UserRegistrationDto>();
            _mockHttpHandler.When(HttpMethod.Post, "/Register")
                .Respond(HttpStatusCode.OK, new StringContent("random"));

            // Act
            var func = async () => await _sut.RegisterNewUser(dto);

            // Assert
            await func.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task RegisterNewUser_ShouldThrow_WhenResponseNotOk()
        {
            // Arrange
            var dto = TestData.GetAutofakedClass<UserRegistrationDto>();
            _mockHttpHandler.When(HttpMethod.Post, "/Register")
                .Respond(HttpStatusCode.BadRequest, new StringContent("random"));

            // Act
            var func = async () => await _sut.RegisterNewUser(dto);

            // Assert
            await func.ShouldThrowAsync<BadHttpRequestException>();
        }
    }
}
