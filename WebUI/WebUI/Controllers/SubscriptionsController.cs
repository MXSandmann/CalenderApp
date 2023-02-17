using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class SubscriptionsController : Controller
    {
        private readonly IMapper _mapper;

        public SubscriptionsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("[action]/{id:guid}")]
        public IActionResult Create(Guid id)
        {
            var viewModel = new CreateSubscriptionViewModel { EventId = id };
            return View(viewModel);
        }

        [HttpPost("[action]/{id:guid}")]
        public IActionResult Create(CreateSubscriptionViewModel createSubscriptionViewModel,[FromRoute] Guid id)
        {
            var subscriptionDto = _mapper.Map<SubscriptionDto>(createSubscriptionViewModel);
            subscriptionDto.EventId = id;            
            // Send subscriptionDto to subscriptions service
            return RedirectToAction("Events", "EventsOverview");
        }

        [HttpGet("[action]/{id:guid}")]
        public IActionResult CreateNotification(Guid id)
        {
            var viewModel = new CreateNotificationViewModel { EventId = id };
            return View("CreateNotification", viewModel);
        }

        [HttpPost("[action]/{id:guid}")]
        public IActionResult CreateNotification(CreateNotificationViewModel notificationViewModel, [FromRoute] Guid id)
        {
            //var notificationDto = new();
            return RedirectToAction("Events", "EventsOverview");
        }

    }
}
