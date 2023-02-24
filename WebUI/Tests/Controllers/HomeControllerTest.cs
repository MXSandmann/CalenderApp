using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using WebUI.Clients.Contracts;
using WebUI.Controllers;

namespace Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IEventsClient> _mockEventsClient;

        // System under test
        private readonly HomeController _sut;

        public HomeControllerTest()
        {
            _mockEventsClient= new Mock<IEventsClient>();
            _sut = new HomeController(_mockEventsClient.Object);
        }

        [Fact]
        public async Task IndexGet_ShouldReturnViewResult_WhenOk()
        {
            // Arrange            
            _mockEventsClient.Setup(x => x.GetCalendarEvents()).ReturnsAsync(TestData.GetCalendarEvents());

            // Act
            var result = await _sut.Index();

            // Assert            
            var viewResult = Assert.IsType<ViewResult>(result);
            var jsonString = JsonConvert.SerializeObject(viewResult.ViewData.Values);
            jsonString.ShouldContain("testname1");
            jsonString.ShouldContain("testname2");
        }
    }
}
