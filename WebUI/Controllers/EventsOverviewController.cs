using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class EventsOverviewController : Controller
    {
        private readonly IUserEventService _service;

        public EventsOverviewController(IUserEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var userEvents = await _service.GetUserEvents();
            var userEventViewModels = userEvents.Select(x => UserEventViewModel.ToUserEventViewModel(x)).ToList();
            return View("EventsOverview", userEventViewModels);
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
            return RedirectToAction(nameof(GetAllEvents));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var eventToUpdate = await _service.GetUserEventById(id);
            if (eventToUpdate == null)
                return NotFound($"The event with id {id} not found");
            return View("Create", UserEventViewModel.ToUserEventViewModel(eventToUpdate));
        }

        [HttpPost("[action]/{id:guid?}")]
        public async Task<IActionResult> Edit(UserEventViewModel model)
        {
            await _service.UpdateUserEvent(model.ToUserEvent());
            return RedirectToAction(nameof(GetAllEvents));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.RemoveUserEvent(id);
            return RedirectToAction(nameof(GetAllEvents));
        }
    }
}
