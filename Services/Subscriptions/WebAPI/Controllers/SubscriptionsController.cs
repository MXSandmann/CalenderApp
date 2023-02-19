using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(_mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userEvent = await _subscriptionService.GetSubscriptionById(id);
        var dto = _mapper.Map<SubscriptionDto>(userEvent);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(SubscriptionDto subscriptionDto)
    {
        var newSubscription = await _subscriptionService.CreateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        return Ok(_mapper.Map<SubscriptionDto>(newSubscription));
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
        return Ok(dto);
    }
}
