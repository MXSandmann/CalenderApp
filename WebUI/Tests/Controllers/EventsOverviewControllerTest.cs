using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using WebUI.Clients.Contracts;
using WebUI.Controllers;
using WebUI.Models.Dtos;
using WebUI.Models.ViewModels;
using WebUI.Profiles;
using WebUI.Validators;

namespace Tests.Controllers
{
    public class EventsOverviewControllerTest
    {
        private readonly Mock<IEventsClient> _mockClient;

        // System under test
        private readonly EventsOverviewController _sut;
        private readonly IValidator<CreateUpdateUserEventViewModel> _validator;
        private readonly IMapper _mapper;

        public EventsOverviewControllerTest()
        {
            _mockClient = new Mock<IEventsClient>();
            _validator = new DateValidator();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);
            _sut = new EventsOverviewController(_validator, _mapper, _mockClient.Object);
        }

        [Fact]
        public async Task IndexGet_ShouldReturnViewResult_WhenOk()
        {
            // Arrange
            _mockClient.Setup(x => x.GetUserEvents(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEventDtos());

            // Act
            var result = await _sut.Events(string.Empty);

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GetUserEventViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(2);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _mockClient.Setup(x => x.AddNewUserEvent(It.IsAny<UserEventDto>(), It.IsAny<RecurrencyRuleDto>()))
                .Verifiable();
            _sut.ModelState.AddModelError("Name", "Required");
            var newModel = TestData.GetUserEventViewModel();

            // Act
            var result = await _sut.Create(newModel);

            // Assert            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnBadRequest_WhenValidationIsUnsuccessful()
        {
            // Arrange
            _mockClient.Setup(x => x.AddNewUserEvent(It.IsAny<UserEventDto>(), It.IsAny<RecurrencyRuleDto>()))
                .Verifiable();
            var newModel = TestData.GetInvalidUserEventViewModel();

            // Act
            var result = await _sut.Create(newModel);

            // Assert            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
            string jsonString = JsonConvert.SerializeObject(badRequestResult.Value);
            jsonString.ShouldContain("\"ErrorMessage\":\"End time must after Start time\"");
        }

        [Fact]
        public async Task CreatePost_ShouldRedirect_WhenModelStateIsValid()
        {
            // Arrange            
            _mockClient.Setup(x => x.GetUserEvents(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEventDtos());
            var newModel = TestData.GetUserEventViewModel();

            // Act
            var result = await _sut.Create(newModel);

            // Assert            
            var requestResult = Assert.IsType<RedirectToActionResult>(result);
            requestResult.ActionName.ShouldBe("Events");
        }

        [Fact]
        public async Task EditGet_ShouldRedirect_WhenFound()
        {
            // Arrange
            var userEvent = TestData.GetUserEventDtos().First();
            _mockClient.Setup(x => x.GetUserEventById(It.IsAny<Guid>()))
                .ReturnsAsync(userEvent);

            // Act
            var result = await _sut.Edit(Guid.NewGuid());

            // Assert            
            var requestResult = Assert.IsType<ViewResult>(result);
            requestResult.ViewName.ShouldBe("Create");
            var model = Assert.IsAssignableFrom<CreateUpdateUserEventViewModel>(requestResult.ViewData.Model);

            var startModel = _mapper.Map<CreateUpdateUserEventViewModel>(userEvent);

            model.Id.ShouldBe(startModel.Id);
            model.Name.ShouldBe(startModel.Name);
            model.Category.ShouldBe(startModel.Category);
            model.Place.ShouldBe(startModel.Place);
            model.StartTime.ShouldBe(startModel.StartTime);
            model.EndTime.ShouldBe(startModel.EndTime);
            model.Date.ShouldBe(startModel.Date);
            model.LastDate.ShouldBe(startModel.LastDate);
            model.Description.ShouldBe(startModel.Description);
            model.AdditionalInfo.ShouldBe(startModel.AdditionalInfo);
            model.ImageUrl.ShouldBe(startModel.ImageUrl);
        }
    }
}
