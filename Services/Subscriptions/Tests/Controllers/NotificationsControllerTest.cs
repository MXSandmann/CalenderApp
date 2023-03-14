using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Profiles;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Quartz;
using System.Diagnostics;
using WebAPI.Controllers;

namespace Tests.Controllers;

public class NotificationsControllerTest
{
    private readonly NotificationsController _sut;
    private readonly Mock<ISubscriptionService> _mockService;
    private readonly IMapper _mapper;

    public NotificationsControllerTest()
    {
        _mockService = new Mock<ISubscriptionService>();
        var mockScheduler = new Mock<IScheduler>();
        var mockLogger = new Mock<ILogger<NotificationsController>>();
        var profile = new AutomapperProfile();
        var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
        _mapper = new Mapper(config);
        var activitySource = new ActivitySource("test");
        _sut = new NotificationsController(_mockService.Object, _mapper, mockLogger.Object, mockScheduler.Object, activitySource);
    }

    [Fact]
    public async Task Add_ShouldReturnNotificationDto_WhenAllOk()
    {
        // Arrange
        var notificationDto = TestData.GetNotificationDto();
        var notification = _mapper.Map<Notification>(notificationDto);
        _mockService.Setup(x => x.AddNotification(It.IsAny<Notification>()))
            .ReturnsAsync(notification);
        _mockService.Setup(x => x.GetSubscriptionById(It.IsAny<Guid>()))
            .ReturnsAsync(TestData.GetSubscription());

        // Act
        var result = await _sut.Add(notificationDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedDto = Assert.IsType<NotificationDto>(okResult.Value);
        Assert.Equal(JsonConvert.SerializeObject(notificationDto), JsonConvert.SerializeObject(returnedDto));
    }
}
