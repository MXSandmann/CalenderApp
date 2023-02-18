using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionsClient _subscriptionsClient;

        public SubscriptionsController(IMapper mapper, ISubscriptionsClient subscriptionsClient)
        {
            _mapper = mapper;
            _subscriptionsClient = subscriptionsClient;
        }

        [HttpGet("[action]/{id:guid}")]
        public IActionResult Create(Guid id)
        {
            var viewModel = new CreateSubscriptionViewModel { EventId = id };
            return View(viewModel);
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel createSubscriptionViewModel,[FromRoute] Guid id)
        {
            var subscriptionDto = _mapper.Map<SubscriptionDto>(createSubscriptionViewModel);
            subscriptionDto.EventId = id;            
            await _subscriptionsClient.AddSubscription(subscriptionDto);
            return RedirectToAction("Events", "EventsOverview");
        }

        [HttpGet("[action]/{id:guid}")]
        public IActionResult CreateNotification(Guid id)
        {
            var viewModel = new CreateNotificationViewModel { EventId = id };
            return View("CreateNotification", viewModel);
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> CreateNotification(CreateNotificationViewModel notificationViewModel, [FromRoute] Guid id)
        {
            var notificationDto = _mapper.Map<NotificationDto>(notificationViewModel);
            notificationDto.EventId = id;
            await _subscriptionsClient.AddNotification(notificationDto);
            return RedirectToAction("Events", "EventsOverview");
        }

    }
}
