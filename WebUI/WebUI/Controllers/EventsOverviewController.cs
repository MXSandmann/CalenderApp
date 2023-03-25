using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;

namespace WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class EventsOverviewController : Controller
    {
        private readonly IEventsClient _eventsClient;
        private readonly IValidator<CreateUpdateUserEventViewModel> _validator;
        private readonly IMapper _mapper;

        public EventsOverviewController(IValidator<CreateUpdateUserEventViewModel> validator, IMapper mapper, IEventsClient eventsClient)
        {
            _validator = validator;
            _mapper = mapper;
            _eventsClient = eventsClient;
        }

        [HttpGet]
        public async Task<IActionResult> Events(string sortBy)
        {
            // Add info to ViewBang to add query paremeter for sorting
            ViewBag.SortTimeParameter = string.IsNullOrWhiteSpace(sortBy) ? "Time" : "";
            ViewBag.SortCategoryParameter = "Category";
            ViewBag.SortPlaceParameter = "Place";
            var userEvents = await _eventsClient.GetUserEvents(sortBy);
            var userEventViewModels = _mapper.Map<IEnumerable<GetUserEventViewModel>>(userEvents);
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
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _eventsClient.AddNewUserEvent(_mapper.Map<UserEventDto>(newModel), _mapper.Map<RecurrencyRuleDto>(newModel));
            return RedirectToAction(nameof(Events));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var eventToUpdate = await _eventsClient.GetUserEventById(id);
            return View(nameof(Create), _mapper.Map<CreateUpdateUserEventViewModel>(eventToUpdate));
        }

        [HttpPost("[action]/{id:guid?}")]
        public async Task<IActionResult> Edit(CreateUpdateUserEventViewModel model)
        {
            var validationResult = _validator.Validate(model);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userEventDto = _mapper.Map<UserEventDto>(model);
            var recurrencyRuleDto = _mapper.Map<RecurrencyRuleDto>(model);
            await _eventsClient.UpdateUserEvent(userEventDto, recurrencyRuleDto);
            return RedirectToAction(nameof(Events));
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventsClient.RemoveUserEvent(id);
            return RedirectToAction(nameof(Events));
        }
    }
}
