using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebUI.Clients.Contracts;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IEventsClient _eventsClient;

    public HomeController(IEventsClient eventsClient)
    {
        _eventsClient = eventsClient;
    }    

    public async Task<IActionResult> Index()
    {
        var events = await _eventsClient.GetCalendarEvents();
        ViewData["Events"] = JsonConvert.SerializeObject(events);
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
