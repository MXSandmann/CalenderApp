using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ILogger<SubscriptionsController> _logger;
    private readonly ISubscriptionService _subscriptionService;
    private readonly IMapper _mapper;

    public SubscriptionsController(ILogger<SubscriptionsController> logger, ISubscriptionService subscriptionService, IMapper mapper)
    {
        _logger = logger;
        _subscriptionService = subscriptionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var subscriptions = await _subscriptionService.GetSubscriptions();
        var dtos = _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
        _logger.LogInformation("--> Found subscriptions: {subs}", JsonConvert.SerializeObject(dtos));
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var subscription = await _subscriptionService.GetSubscriptionById(id);
        var dto = _mapper.Map<SubscriptionDto>(subscription);
        _logger.LogInformation("--> Found subscriptions: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(SubscriptionDto subscriptionDto)
    {
        var newSubscription = await _subscriptionService.CreateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        var dto = _mapper.Map<SubscriptionDto>(newSubscription);
        _logger.LogInformation("--> Created subscription: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }

    [HttpDelete("[action]/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _subscriptionService.RemoveSubscription(id);
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromBody] SubscriptionDto subscriptionDto)
    {
        var subscription = await _subscriptionService.UpdateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        var dto = _mapper.Map<SubscriptionDto>(subscription);
        _logger.LogInformation("--> Updating subscription: {subs}", JsonConvert.SerializeObject(dto));
        return Ok(dto);
    }
}
