using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebUI.Clients.Contracts;
using WebUI.Controllers;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;
using WebUI.Models.ViewModels;
using WebUI.Profiles;
using WebUI.Validators;

namespace Tests.Controllers
{
    public class InvitationsControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateInvitationViewModel> _validator;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IInvitationsClient> _mockInvitationsClient;
        private readonly InvitationsController _sut;

        public InvitationsControllerTest()
        {
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);
            _mockInvitationsClient = new();
            _mockHttpContextAccessor = new();
            _validator = new InvitationValidator();
            _sut = new(_mockInvitationsClient.Object, _mapper, _mockHttpContextAccessor.Object, _validator);
        }

        [Fact]
        public async Task CreateToEvent_WhenInvalidModelState_ShouldReturnsBadRequest()
        {
            // Arrange
            _sut.ModelState.AddModelError("Test", "Test");
            var viewModel = new CreateInvitationViewModel();

            // Act
            var result = await _sut.CreateToEvent(viewModel, Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateToEvent_ValidRequest_ReturnsRedirectToActionResult()
        {
            // Arrange
            var viewModel = new CreateInvitationViewModel()
            {
                Email = "Test@gmail.com",
                Role = RoleToInvite.User
            };
            _mockInvitationsClient.Setup(x => x.CreateInvitation(It.IsAny<InvitationDto>())).ReturnsAsync(new InvitationDto());
            SetupUserClaims();

            // Act
            var result = await _sut.CreateToEvent(viewModel, Guid.NewGuid());

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectResult?.ControllerName);
            Assert.Equal("Index", redirectResult?.ActionName);
        }

        private void SetupUserClaims()
        {
            var userName = "TestUser";
            var claims = new List<Claim>
            {
                new Claim("UserName", userName)
            };
            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext { User = principal };
            _mockHttpContextAccessor.Setup(h => h.HttpContext).Returns(httpContext);
        }

        [Fact]
        public async Task Create_WhenInvalidModelState_ShouldReturnsBadRequest()
        {
            // Arrange
            _sut.ModelState.AddModelError("Test", "Test");
            var viewModel = new CreateInvitationViewModel();

            // Act
            var result = await _sut.Create(viewModel);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ValidRequest_ReturnsRedirectToActionResult()
        {
            // Arrange
            var viewModel = new CreateInvitationViewModel()
            {
                Email = "Test@gmail.com",
                Role = RoleToInvite.Instructor
            };
            _mockInvitationsClient.Setup(x => x.CreateInvitation(It.IsAny<InvitationDto>())).ReturnsAsync(new InvitationDto());
            SetupUserClaims();

            // Act
            var result = await _sut.Create(viewModel);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectResult?.ControllerName);
            Assert.Equal("Index", redirectResult?.ActionName);
        }
    }
}
