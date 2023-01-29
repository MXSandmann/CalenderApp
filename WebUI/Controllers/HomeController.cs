using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IUserEventService _service;

    public HomeController(IUserEventService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var events = await _service.GetCalendarEvents();
        ViewData["Events"] = JsonSerializer.Serialize(events);
        return View();
    }

    //public async Task<IActionResult> Index()
    //{
    //    var userEvents = await _service.GetUserEvents(string.Empty);        
    //    ViewData["Events"] = EventParseHelper.SerializeUserEvensToJsonString(userEvents);
    //    return View();
    //}

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
