using ApplicationCore.Models;
using ApplicationCore.Services.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using WebUI.Controllers;
using WebUI.Models;
using WebUI.Validators;

namespace Tests.Controllers
{
    public class EventsOverviewControllerTest
    {
        private readonly Mock<IUserEventService> _serviceMock;

        // System under test
        private readonly EventsOverviewController _sut;
        private readonly IValidator<UserEventViewModel> _validator;

        public EventsOverviewControllerTest()
        {
            _serviceMock = new Mock<IUserEventService>();
            _validator = new DateValidator();
            _sut = new EventsOverviewController(_serviceMock.Object, _validator);
        }

        [Fact]
        public async Task IndexGet_ShouldReturnViewResult_WhenOk()
        {
            // Arrange
            _serviceMock.Setup(x => x.GetUserEvents(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var result = await _sut.Events(string.Empty);

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserEventViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(2);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _serviceMock.Setup(x => x.AddNewUserEvent(It.IsAny<UserEvent>()))
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
        public async Task CreatePost_ShouldRedirect_WhenModelStateIsValid()
        {
            // Arrange            
            _serviceMock.Setup(x => x.GetUserEvents(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEvents());
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
            var userEvent = TestData.GetUserEvents().First();
            _serviceMock.Setup(x => x.GetUserEventById(It.IsAny<Guid>()))
                .ReturnsAsync(userEvent);

            // Act
            var result = await _sut.Edit(Guid.NewGuid());

            // Assert            
            var requestResult = Assert.IsType<ViewResult>(result);
            requestResult.ViewName.ShouldBe("Create");
            var model = Assert.IsAssignableFrom<UserEventViewModel>(requestResult.ViewData.Model);

            var startModel = UserEventViewModel.ToUserEventViewModel(userEvent);
                        
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
