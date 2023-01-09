using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZeroTask.BLL.Services.Contracts;
using ZeroTask.PL.Models;

namespace ZeroTask.PL.Controllers;

public class HomeController : Controller
{
    private readonly IUserEventService _service;

    public HomeController(IUserEventService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var userEvents = await _service.GetUserEvents();
        var userEventViewModels = userEvents.Select(x => UserEventViewModel.ToUserEventViewModel(x)).ToList();
        return View(userEventViewModels);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserEventViewModel newModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.AddNewUserEvent(newModel.ToUserEvent());
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var eventToUpdate = await _service.GetUserEventById(id);
        if (eventToUpdate == null)
            return NotFound($"The event with id {id} not found");
        return View("Create", UserEventViewModel.ToUserEventViewModel(eventToUpdate));
    }

    [HttpPost("[action]/{id:int?}")]
    public async Task<IActionResult> Edit(UserEventViewModel model)
    {
        await _service.UpdateUserEvent(model.ToUserEvent());
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("[action]/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.RemoveUserEvent(id);
        return RedirectToAction(nameof(Index));
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
