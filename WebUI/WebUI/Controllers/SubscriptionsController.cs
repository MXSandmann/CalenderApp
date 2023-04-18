using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SubscriptionsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionsClient _subscriptionsClient;
        private readonly IEventsClient _eventClient;
        private readonly IValidator<CreateSubscriptionViewModel> _validator;
        private readonly ILogger<SubscriptionsController> _logger;

        public SubscriptionsController(IMapper mapper, ISubscriptionsClient subscriptionsClient, IValidator<CreateSubscriptionViewModel> validator, IEventsClient eventClient, ILogger<SubscriptionsController> logger)
        {
            _mapper = mapper;
            _subscriptionsClient = subscriptionsClient;
            _validator = validator;
            _eventClient = eventClient;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SubscriptionsOverview()
        {
            var subscriptions = await _subscriptionsClient.GetAllSubscriptions();
            _logger.LogInformation("--> Recieved subscriptions: {subscription}", JsonConvert.SerializeObject(subscriptions));

            await _eventClient.AddUserEventNamesForSubscriptions(subscriptions);
            _logger.LogInformation("--> Merging subscriptions with event names: {subscription}", JsonConvert.SerializeObject(subscriptions));

            var viewModels = _mapper.Map<IEnumerable<GetSubscriptionViewModel>>(subscriptions);
            return View(viewModels);
        }

        [HttpGet("[action]/{eventId:guid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Create(Guid eventId)
        {
            var viewModel = new CreateSubscriptionViewModel { EventId = eventId };
            return View(viewModel);
        }

        [HttpPost("[action]/{eventId:guid}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel createSubscriptionViewModel, [FromRoute] Guid eventId)
        {
            var validationResult = _validator.Validate(createSubscriptionViewModel);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            var subscriptionDto = _mapper.Map<SubscriptionDto>(createSubscriptionViewModel);
            subscriptionDto.EventId = eventId;
            _logger.LogInformation("--> Adding subscription {subscription}", JsonConvert.SerializeObject(subscriptionDto));
            await _subscriptionsClient.AddSubscription(subscriptionDto);
            return RedirectToAction(nameof(SubscriptionsOverview));
        }

        [HttpGet("[action]/{subscriptionId:guid}")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult CreateNotification(Guid subscriptionId)
        {
            var viewModel = new CreateNotificationViewModel { SubscriptionId = subscriptionId };
            return View(nameof(CreateNotification), viewModel);
        }

        [HttpPost("[action]/{subscriptionId:guid}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateNotification(CreateNotificationViewModel notificationViewModel)
        {
            // Request some data
            var subscription = await _subscriptionsClient.GetSubscriptionById(notificationViewModel.SubscriptionId);
            var userEvent = await _eventClient.GetUserEventById(subscription.EventId);

            var notificationDto = _mapper.Map<NotificationDto>(notificationViewModel);
            notificationDto.NotificationTime = CalculateNotificationTime(notificationViewModel.NotificationTimeSpan, userEvent);
            notificationDto.EventName = userEvent.Name;

            _logger.LogInformation("--> Adding notification {notification}", JsonConvert.SerializeObject(notificationDto));
            await _subscriptionsClient.AddNotification(notificationDto);
            return RedirectToAction(nameof(SubscriptionsOverview));
        }

        private static DateTime CalculateNotificationTime(TimeSpan notificationTimeSpan, UserEventDto userEvent)
        {
            var time = userEvent.StartTime.TimeOfDay;
            var date = userEvent.Date.Date.Add(time).Add(-notificationTimeSpan);
            return date;
        }

        [HttpGet("[action]/{id:guid}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subscriptionsClient.RemoveSubscription(id);
            return RedirectToAction(nameof(SubscriptionsOverview));
        }

        [HttpGet("[action]/{id:guid}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var subscriptionToUpdate = await _subscriptionsClient.GetSubscriptionById(id);
            return View(nameof(Create), _mapper.Map<CreateSubscriptionViewModel>(subscriptionToUpdate));
        }

        [HttpPost("[action]/{id:guid}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Edit(CreateSubscriptionViewModel model, [FromRoute] Guid id)
        {
            var validationResult = _validator.Validate(model);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            model.SubscriptionId = id;
            var subscriptionDto = _mapper.Map<SubscriptionDto>(model);
            _logger.LogInformation("--> Updateing subscription {subscription}", JsonConvert.SerializeObject(subscriptionDto));
            await _subscriptionsClient.UpdateSubscription(subscriptionDto);
            return RedirectToAction(nameof(SubscriptionsOverview));
        }
    }
}
