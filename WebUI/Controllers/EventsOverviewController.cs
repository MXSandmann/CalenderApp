using ApplicationCore.Services.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class EventsOverviewController : Controller
    {
        private readonly IUserEventService _service;
        private readonly IValidator<CreateUpdateUserEventViewModel> _validator;

        public EventsOverviewController(IUserEventService service, IValidator<CreateUpdateUserEventViewModel> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Events(string sortBy)
        {
            // Add info to ViewBang to add query paremeter for sorting
            ViewBag.SortTimeParameter = string.IsNullOrWhiteSpace(sortBy) ? "Time" : "";
            ViewBag.SortCategoryParameter = "Category";
            ViewBag.SortPlaceParameter = "Place";
            var userEvents = await _service.GetUserEvents(sortBy);
            var userEventViewModels = userEvents.Select(x => GetUserEventViewModel.ToUserEventViewModel(x)).ToList();
            return View("EventsOverview", userEventViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateUserEventViewModel newModel)
        {
            var validationResult = _validator.Validate(newModel);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _service.AddNewUserEvent(newModel.ToUserEvent(), newModel.ToRecurrencyRule());
            return RedirectToAction(nameof(Events));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var eventToUpdate = await _service.GetUserEventById(id);
            if (eventToUpdate == null)
                return NotFound($"The event with id {id} not found");
            return View("Create", CreateUpdateUserEventViewModel.ToUserEventViewModel(eventToUpdate));
        }

        [HttpPost("[action]/{id:guid?}")]
        public async Task<IActionResult> Edit(CreateUpdateUserEventViewModel model)
        {
            var validationResult = _validator.Validate(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _service.UpdateUserEvent(model.ToUserEvent(), model.ToRecurrencyRule());
            return RedirectToAction(nameof(Events));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.RemoveUserEvent(id);
            return RedirectToAction(nameof(Events));
        }
    }
}
