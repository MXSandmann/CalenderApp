using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Clients;
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
        private readonly IValidator<CreateSubscriptionViewModel> _validator;

        public SubscriptionsController(IMapper mapper, ISubscriptionsClient subscriptionsClient, IValidator<CreateSubscriptionViewModel> validator)
        {
            _mapper = mapper;
            _subscriptionsClient = subscriptionsClient;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> SubscriptionOverview()
        {
            var subscriptions = await _subscriptionsClient.GetAllSubscriptions();
            var viewModels = _mapper.Map<IEnumerable<GetSubscriptionViewModel>>(subscriptions);
            return View(viewModels);
        }

        [HttpGet("[action]/{eventId:guid}")]
        public IActionResult Create(Guid eventId)
        {
            var viewModel = new CreateSubscriptionViewModel { EventId = eventId };
            return View(viewModel);
        }

        [HttpPost("[action]/{eventId:guid}")]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel createSubscriptionViewModel,[FromRoute] Guid eventId)
        {
            var validationResult = _validator.Validate(createSubscriptionViewModel);
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);            
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var subscriptionDto = _mapper.Map<SubscriptionDto>(createSubscriptionViewModel);
            subscriptionDto.EventId = eventId;            
            await _subscriptionsClient.AddSubscription(subscriptionDto);
            return RedirectToAction(nameof(SubscriptionOverview));
        }

        [HttpGet("[action]/{subscriptionId:guid}")]
        public IActionResult CreateNotification(Guid subscriptionId)
        {
            var viewModel = new CreateNotificationViewModel { SubscriptionId = subscriptionId };
            return View(nameof(CreateNotification), viewModel);
        }

        [HttpPost("[action]/{subscriptionId:guid}")]
        public async Task<IActionResult> CreateNotification(CreateNotificationViewModel notificationViewModel, [FromRoute] Guid subscriptionId)
        {
            var notificationDto = _mapper.Map<NotificationDto>(notificationViewModel);
            notificationDto.SubscriptionId = subscriptionId;
            await _subscriptionsClient.AddNotification(notificationDto);
            return RedirectToAction(nameof(SubscriptionOverview));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subscriptionsClient.RemoveSubscription(id);
            return RedirectToAction(nameof(SubscriptionOverview));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var subscriptionToUpdate = await _subscriptionsClient.GetSubscriptionById(id);
            return View(nameof(Create), _mapper.Map<CreateSubscriptionViewModel>(subscriptionToUpdate));
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(CreateSubscriptionViewModel model, [FromRoute] Guid id)
        {
            var validationResult = _validator.Validate(model);
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);            
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            model.SubscriptionId = id;
            var subscriptionDto = _mapper.Map<SubscriptionDto>(model);
            await _subscriptionsClient.UpdateSubscription(subscriptionDto);
            return RedirectToAction(nameof(SubscriptionOverview));
        }
    }
}
