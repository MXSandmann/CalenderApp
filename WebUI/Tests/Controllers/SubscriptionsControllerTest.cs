using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using WebUI.Clients.Contracts;
using WebUI.Controllers;
using WebUI.Models;
using WebUI.Models.Dtos;
using WebUI.Profiles;
using WebUI.Validators;

namespace Tests.Controllers
{
    public class SubscriptionsControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSubscriptionViewModel> _validator;
        private readonly Mock<ISubscriptionsClient> _mockSubscriptionsClient;
        private readonly Mock<IEventsClient> _mockEventClient;
        private readonly SubscriptionsController _sut;
        private readonly Mock<ILogger<SubscriptionsController>> _mockLogger;

        public SubscriptionsControllerTest()
        {
            _mockSubscriptionsClient = new();
            _mockEventClient = new();
            _mockLogger = new();
            _validator = new SubscriptionValidator();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);
            _sut = new(_mapper, _mockSubscriptionsClient.Object, _validator, _mockEventClient.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SubscriptionOverview_ShouldShowSubscriptionsWithEventNames_WhenAllOk()
        {
            // Arrange
            _mockSubscriptionsClient.Setup(x => x.GetAllSubscriptions())
                .ReturnsAsync(TestData.GetSubscriptionDtos());
            _mockEventClient.Setup(x => x.AddUserEventNamesForSubscriptions(It.IsAny<IEnumerable<SubscriptionDto>>()))
                .Verifiable();

            // Act
            var result = await _sut.SubscriptionsOverview();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GetSubscriptionViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(2);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var invalidViewModel = new CreateSubscriptionViewModel
            {
                UserEmail = "invalid",
                UserName = "G"
            };

            // Act
            var result = await _sut.Create(invalidViewModel, Guid.NewGuid());

            // Assert            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var valueString = JsonConvert.SerializeObject(badRequestResult.Value);
            valueString.ShouldContain("'User Email' is not a valid email address.");
        }
    }
}