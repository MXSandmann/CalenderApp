using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebUI.Clients.Contracts;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IEventsClient _eventsClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IEventsClient eventsClient, ILogger<HomeController> logger)
    {
        _eventsClient = eventsClient;
        _logger = logger;
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
    public IActionResult SmartSearch(SmartSearchViewModel viewModel)
    {
        _logger.LogInformation("Received search entry: {entry}", viewModel.Entry);
        return View();
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
