using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
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
        return Ok(_mapper.Map<SubscriptionDto>(subscriptions));
    }

    [HttpPost]
    public async Task<IActionResult> AddSubscription(SubscriptionDto subscriptionDto)
    {
        var newSubscription = await _subscriptionService.CreateSubscription(_mapper.Map<Subscription>(subscriptionDto));
        return Ok(_mapper.Map<SubscriptionDto>(newSubscription));

    }
}
