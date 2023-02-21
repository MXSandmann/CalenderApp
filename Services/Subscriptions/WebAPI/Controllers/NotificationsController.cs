using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models.Dtos;
using Newtonsoft.Json;
using ApplicationCore.Factories;
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
        public async Task<IActionResult> Add(NotificationDto notificationDto, CancellationToken cancellationToken)
        {
            var newNotification = await _subscriptionService.AddNotification(_mapper.Map<Notification>(notificationDto));
            var subscription = await _subscriptionService.GetSubscriptionById(newNotification.SubscriptionId);

            await NotificationsFactory.ScheduleEmail(_scheduler, subscription.UserEmail, subscription.UserName, DateTime.Now.AddSeconds(5), cancellationToken);

            _logger.LogInformation("--> Created Notification: {not}", JsonConvert.SerializeObject(newNotification));
            return Ok(_mapper.Map<NotificationDto>(newNotification));           
        }       
    }
}
