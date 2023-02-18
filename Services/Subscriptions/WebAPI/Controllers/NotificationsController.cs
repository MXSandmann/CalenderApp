using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models.Dtos;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public NotificationsController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NotificationDto notificationDto)
        {
            var newNotification = await _subscriptionService.AddNotification(_mapper.Map<Notification>(notificationDto));
            return Ok(_mapper.Map<NotificationDto>(newNotification));

        }
    }
}
