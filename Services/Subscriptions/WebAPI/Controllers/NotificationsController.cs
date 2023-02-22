using ApplicationCore.Factories;
using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationsController> _logger;
        private readonly IScheduler _scheduler;

        public NotificationsController(ISubscriptionService subscriptionService, IMapper mapper, ILogger<NotificationsController> logger, IScheduler scheduler)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
            _logger = logger;
            _scheduler = scheduler;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NotificationDto notificationDto, CancellationToken cancellationToken)
        {
            var newNotification = await _subscriptionService.AddNotification(_mapper.Map<Notification>(notificationDto));
            var subscription = await _subscriptionService.GetSubscriptionById(newNotification.SubscriptionId);

            // Get the difference between local time and utc time
            var timeDif = DateTime.Now - DateTime.UtcNow;
            // Add the offset
            var fireTime = newNotification.NotificationTime.Add(-timeDif);

            await NotificationsFactory.ScheduleEmail(_scheduler, subscription.UserEmail, subscription.UserName, fireTime, newNotification.Id, cancellationToken);
            var dto = _mapper.Map<NotificationDto>(newNotification);
            _logger.LogInformation("--> Created Notification: {not}", JsonConvert.SerializeObject(dto));
            return Ok(dto);
        }
    }
}
