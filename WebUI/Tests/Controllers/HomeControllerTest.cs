
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System.Diagnostics;
using WebUI.Clients.Contracts;
using WebUI.Controllers;
using WebUI.Profiles;

namespace Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IEventsClient> _mockEventsClient;

        // System under test
        private readonly HomeController _sut;

        public HomeControllerTest()
        {
            _mockEventsClient = new Mock<IEventsClient>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            var mockActivitySourse = new ActivitySource("test");
            _sut = new HomeController(_mockEventsClient.Object, mockLogger.Object, mapper, mockActivitySourse);
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
