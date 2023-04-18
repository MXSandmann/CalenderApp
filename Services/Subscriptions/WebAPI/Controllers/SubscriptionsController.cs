using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionsController : ControllerBase
{
    private readonly ILogger<SubscriptionsController> _logger;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IMapper _mapper;
    private readonly ActivitySource _activitySource;

    public SubscriptionsController(ILogger<SubscriptionsController> logger, ISubscriptionService subscriptionService, IMapper mapper, ActivitySource activitySource)
    {
        _logger = logger;
        _subscriptionService = subscriptionService;
        _mapper = mapper;
        _activitySource = activitySource;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        using var activity = _activitySource.StartActivity($"{nameof(SubscriptionsController)}: {nameof(Get)} action");
        var subscriptions = await _subscriptionService.GetSubscriptions();
        var dtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        _logger.LogInformation("--> Found subscriptions: {subs}", JsonConvert.SerializeObject(dtos));
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        using var activity = _activitySource.StartActivity($"{nameof(SubscriptionsController)}: {nameof(GetById)} action");
        var subscription = await _subscriptionService.GetSubscriptionById(id);
        var dto = _mapper.Map<SubscriptionDto>(subscription);
        _logger.LogInformation("--> Found subscriptions: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(SubscriptionDto subscriptionDto)
    {
        using var activity = _activitySource.StartActivity($"{nameof(SubscriptionsController)}: {nameof(AddSubscription)} action");
        var newSubscription = await _subscriptionService.CreateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        var dto = _mapper.Map<SubscriptionDto>(newSubscription);
        _logger.LogInformation("--> Created subscription: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        using var activity = _activitySource.StartActivity($"{nameof(SubscriptionsController)}: {nameof(Remove)} action");
        await _subscriptionService.RemoveSubscription(id);
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromBody] SubscriptionDto subscriptionDto)
    {
        using var activity = _activitySource.StartActivity($"{nameof(SubscriptionsController)}: {nameof(Update)} action");
        var subscription = await _subscriptionService.UpdateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        var dto = _mapper.Map<SubscriptionDto>(subscription);
        _logger.LogInformation("--> Updating subscription: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }
}
