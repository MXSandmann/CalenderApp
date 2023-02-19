using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models.Dtos;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(ISubscriptionService subscriptionService, IMapper mapper, ILogger<NotificationsController> logger)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NotificationDto notificationDto)
        {
            var newNotification = await _subscriptionService.AddNotification(_mapper.Map<Notification>(notificationDto));
            _logger.LogInformation("--> Created Notification: {not}", JsonConvert.SerializeObject(newNotification));
            return Ok(_mapper.Map<NotificationDto>(newNotification));

        }
    }
}
