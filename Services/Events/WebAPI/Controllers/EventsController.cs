using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IUserEventService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<EventsController> _logger;
        private readonly ActivitySource _activitySource;

        public EventsController(IUserEventService service, IMapper mapper, ILogger<EventsController> logger, ActivitySource activitySource)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _activitySource = activitySource;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(Get)} action");
            var userEvents = await _service.GetUserEvents();
            var dtos = _mapper.Map<IEnumerable<UserEventDto>>(userEvents);
            _logger.LogInformation("--> Found user events: {ue}", JsonConvert.SerializeObject(dtos));
            return Ok(dtos);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UserEventDto userEventDto)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(Create)} action");
            var userEvent = await _service.AddNewUserEvent(_mapper.Map<UserEvent>(userEventDto), _mapper.Map<RecurrencyRule>(userEventDto.RecurrencyRule));
            var dto = _mapper.Map<UserEventDto>(userEvent);
            _logger.LogInformation("--> Created user event: {ue}", JsonConvert.SerializeObject(dto));
            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(GetById)} action");
            var userEvent = await _service.GetUserEventById(id);
            var dto = _mapper.Map<UserEventDto>(userEvent);
            _logger.LogInformation("--> Found user event: {ue}", JsonConvert.SerializeObject(dto));
            return Ok(dto);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UserEventDto userEventDto)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(Update)} action");
            var userEvent = await _service.UpdateUserEvent(_mapper.Map<UserEvent>(userEventDto), _mapper.Map<RecurrencyRule>(userEventDto.RecurrencyRule));
            var dto = _mapper.Map<UserEventDto>(userEvent);
            _logger.LogInformation("--> Updated user event: {ue}", JsonConvert.SerializeObject(dto));
            return Ok(dto);
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(Remove)} action");
            await _service.RemoveUserEvent(id);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventNames([FromBody] IEnumerable<Guid> eventIds)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(EventNames)} action");
            var dict = await _service.GetEventNames(eventIds);
            _logger.LogInformation("--> Found following event names: {dict}", JsonConvert.SerializeObject(dict));
            return Ok(dict);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery] string entry, int limit, int offset)
        {
            using var activity = _activitySource.StartActivity($"{nameof(EventsController)}: {nameof(Search)} action");
            _logger.LogInformation("--> Searching for entry \"{value1}\", with limit {value2} with offset {value3}", entry, limit, offset);
            var results = await _service.SearchUserEvents(entry, limit, offset);
            _logger.LogInformation("--> Search result with entry \"{value1}\": {value2}", entry, JsonConvert.SerializeObject(results));
            return Ok(results);
        }

        [HttpGet("[action]/{eventId:guid}")]
        public async Task<IActionResult> Download(Guid eventId)
        {
            var icsFileString = await _service.DownloadICSFile(eventId);
            var icsByteArray = Encoding.UTF8.GetBytes(icsFileString);

            var memoryStream = new MemoryStream(icsByteArray);

            return new FileStreamResult(memoryStream, "text/calendar")
            {
                FileDownloadName = "my-event.ics"
            };
        }
    }
}
