using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTelemetry;
using System.Diagnostics;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IEventsClient _eventsClient;
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly ActivitySource _activitySource;

    public HomeController(IEventsClient eventsClient, ILogger<HomeController> logger, IMapper mapper, ActivitySource activitySource)
    {
        _eventsClient = eventsClient;
        _logger = logger;
        _mapper = mapper;
        _activitySource = activitySource;
    }

    public async Task<IActionResult> Index()
    {
        using (var activity = _activitySource.StartActivity("Request Activity"))
        {
            activity?.SetTag("Action_Name", nameof(Index));
            activity?.AddEvent(new ActivityEvent("Start pulling all user events"));
        }
        var events = await _eventsClient.GetCalendarEvents();
        
        using (var activity = _activitySource.StartActivity("Received Activity"))
        {
            var items = activity?.GetBaggageItem("calendar_events");
            var items2 = Activity.Current?.GetBaggageItem("calendar_events");
            var _items = Baggage.GetBaggage("calendar_events");

            

            
            Console.WriteLine($"--> items: {items}, {_items}, {items2}");
            activity?.SetTag("calendar_events", items);
        }
        _logger.LogInformation("Received events: {events}", JsonConvert.SerializeObject(events));
        ViewData["Events"] = JsonConvert.SerializeObject(events);
        return View();
    }

    [HttpGet]
    public IActionResult SmartSearch()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SmartSearch(SmartSearchViewModel viewModel)
    {
        _logger.LogInformation("Received search entry: {entry}", viewModel.Entry);
        var (events, count) = await _eventsClient.GetSearchResults(_mapper.Map<SearchUserEventsDto>(viewModel));
        viewModel.UserEvents = _mapper.Map<IEnumerable<GetUserEventViewModel>>(events);
        viewModel.Count = count;
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
