using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroTask.BLL.Services.Contracts;
using ZeroTask.PL.Controllers;
using ZeroTask.PL.Models;

namespace ZeroTask.Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IUserEventService> _serviceMock;

        // System under test
        private readonly HomeController _sut;

        public HomeControllerTest()
        {
            _serviceMock = new Mock<IUserEventService>();
            _sut = new HomeController(_serviceMock.Object);
        }

        [Fact]
        public async Task IndexGet_ShouldReturnViewResult_WhenOk()
        {
            // Arrange
            _serviceMock.Setup(x => x.GetUserEvents())
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var result = await _sut.Index();

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserEventViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(2);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _serviceMock.Setup(x => x.GetUserEvents())
                .ReturnsAsync(TestData.GetUserEvents());
            _sut.ModelState.AddModelError("Name", "Required");
             
            var newEvent = TestData.GetUserEvents().First();
            var newModel = UserEventViewModel.ToUserEventViewModel(newEvent);

            // Act
            var result = await _sut.Create(newModel);

            // Assert            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);            
        }

        [Fact]
        public async Task CreatePost_Should_WhenModelStateIsValid()
        {
            // Arrange
            _serviceMock.Setup(x => x.GetUserEvents())
                .ReturnsAsync(TestData.GetUserEvents());            

            var newEvent = TestData.GetUserEvents().First();
            var newModel = UserEventViewModel.ToUserEventViewModel(newEvent);

            // Act
            var result = await _sut.Create(newModel);

            // Assert            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}
