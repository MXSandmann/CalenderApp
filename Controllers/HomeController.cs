using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZeroTask.Data;
using ZeroTask.Models;

namespace ZeroTask.Controllers;

public class HomeController : Controller
{    
    private readonly DataContext _context;

    public HomeController(DataContext context)
    { 
        _context = context;
    }

    public IActionResult Index()
    {        
        return View(_context.EventViewModels.ToList());        
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EventViewModel newModel)
    {
        if (!ModelState.IsValid)
        {          
            var errors = ModelState.Select(x => x.Value?.Errors)
                .Where(x => x?.Count > 0)  
                .SelectMany(x => x!.Select(y => y.ErrorMessage))
                .ToList();
            return BadRequest(string.Join('\n', errors));
        }
        _context.EventViewModels.Add(newModel);
        _context.SaveChanges();        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id:int}")]
    public IActionResult Edit(int id)
    {
        var eventToUpdate = _context.EventViewModels.Find(id);
        if (eventToUpdate == null)
            return NotFound($"The event with id {id} not found");
        return View("Create", eventToUpdate);
    }

    [HttpPost("Edit/{id:int?}")]
    public IActionResult Edit(EventViewModel model)
    {
        _context.EventViewModels.Update(model);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var eventToDelete = _context.EventViewModels.Find(id);
        if (eventToDelete == null)
            return BadRequest($"The event with id {id} not found");
        _context.EventViewModels.Remove(eventToDelete);
        _context.SaveChanges();
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
