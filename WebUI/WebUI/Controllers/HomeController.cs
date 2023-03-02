using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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

    public HomeController(IEventsClient eventsClient, ILogger<HomeController> logger, IMapper mapper)
    {
        _eventsClient = eventsClient;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _eventsClient.GetCalendarEvents();
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
