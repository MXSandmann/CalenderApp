using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using WebUI.Controllers;
using WebUI.Models;

namespace Tests.Controllers
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
            _serviceMock.Setup(x => x.GetUserEvents(It.IsAny<string>()))
                .ReturnsAsync(TestData.GetUserEvents());

            // Act
            var result = await _sut.Index();

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserEventViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(2);
        }        
    }
}
