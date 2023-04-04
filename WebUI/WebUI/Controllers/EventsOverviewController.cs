using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Extensions;
using WebUI.Models.Dtos;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    [Authorize]
    public class EventsOverviewController : Controller
    {
        private readonly IEventsClient _eventsClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly IValidator<CreateUpdateUserEventViewModel> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<EventsOverviewController> _logger;

        public EventsOverviewController(IValidator<CreateUpdateUserEventViewModel> validator, IMapper mapper, IEventsClient eventsClient, IAuthenticationClient authenticationClient, ILogger<EventsOverviewController> logger)
        {
            _validator = validator;
            _mapper = mapper;
            _eventsClient = eventsClient;
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Events(string sortBy)
        {
            // Add info to ViewBang to add query paremeter for sorting
            ViewBag.SortTimeParameter = string.IsNullOrWhiteSpace(sortBy) ? "Time" : "";
            ViewBag.SortCategoryParameter = "Category";
            ViewBag.SortPlaceParameter = "Place";

            var userId = User.GetInstructorIdFromClaims();

            var userEvents = await _eventsClient.GetUserEvents(sortBy, userId);
            var userEventViewModels = _mapper.Map<IEnumerable<GetUserEventViewModel>>(userEvents);
            return View("EventsOverview", userEventViewModels);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var eventToUpdate = await _eventsClient.GetUserEventById(id);
            return View(nameof(Create), _mapper.Map<CreateUpdateUserEventViewModel>(eventToUpdate));
        }

        [HttpPost("[action]/{id:guid?}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eventsClient.RemoveUserEvent(id);
            return RedirectToAction(nameof(Events));
        }

        [HttpGet("[action]/{id:guid}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Download(Guid id)
        {
            var fileDto = await _eventsClient.DownloadEventAsFile(id);
            if (fileDto == null)
                return BadRequest("Unable to generate the .ics file");

            return new FileStreamResult(fileDto.ContentStream, fileDto.ContentType)
            {
                FileDownloadName = fileDto.FileName
            };
        }

        [HttpGet("[action]/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign()
        {
            var instructors = await _authenticationClient.GetAllInstructors();
            _logger.LogInformation("--> Received instructors: {value}", JsonConvert.SerializeObject(instructors));
            var viewModel = new AssignInstructorViewModel
            {
                Instructors = new SelectList(instructors, "Id", "Name")
            };
            return View(viewModel);
        }

        [HttpPost("[action]/{eventId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign(AssignInstructorViewModel viewModel, Guid eventId)
        {
            _logger.LogInformation("--> view model: {value}", JsonConvert.SerializeObject(viewModel));
            ModelState.Remove(nameof(viewModel.Instructors));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                        
            try
            {
                var userEvent = await _eventsClient.AssignInstructorToEvent(eventId, viewModel.InstructorId);
                if (userEvent.InstructorId == null 
                    || userEvent.InstructorId.Value == Guid.Empty)
                    throw new ArgumentException(nameof(userEvent));
            }
            catch
            {
                ViewData["Message"] = "Error during assigning instructor to event";
                return View("AssignResult");
            }
            ViewData["Message"] = $"Instructor with id: {viewModel.InstructorId} has been successfuly assigned to event";
            return View("AssignResult");
        }

        [HttpGet("[action]/{eventId:guid}")]
        public async Task<IActionResult> MarkAsDone(Guid eventId)
        {
            _logger.LogInformation("--> Event with id: {value} has been marked as done", eventId);
            return RedirectToAction(nameof(Events));
        }
    }
}
