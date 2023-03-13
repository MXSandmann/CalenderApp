using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Runtime.CompilerServices;
using WebUI.Clients.Contracts;
using WebUI.Controllers;
using WebUI.Models;
using WebUI.Profiles;

namespace Tests.Controllers
{
    public class ActivitiesControllerTest
    {
        private readonly Mock<IEventsClient> _mockEventsClient;
        private readonly Mock<ISubscriptionsClient> _mockSubscriptionsClient;
        private readonly ActivitiesController _sut;

        public ActivitiesControllerTest()
        {
            _mockEventsClient = new Mock<IEventsClient>();
            _mockSubscriptionsClient = new Mock<ISubscriptionsClient>();
            var profile = new AutomapperProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(config);
            _sut = new(_mockEventsClient.Object, _mockSubscriptionsClient.Object, mapper);
        }

        [Fact]
        public async Task Activities_ShouldReturnsActivities_WhenAllOk()
        {
            // Arrange
            _mockEventsClient.Setup(x => x.GetAllActivities())
                .ReturnsAsync(TestData.GetUserActivityRecords(5));
            _mockSubscriptionsClient.Setup(x => x.GetAllActivities())
                .ReturnsAsync(TestData.GetUserActivityRecords(6));

            // Act
            var result = await _sut.Activities();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ActivitiesOverviewViewModel>>(viewResult.ViewData.Model);
            model.Count().ShouldBe(11);
        }

    }
}
