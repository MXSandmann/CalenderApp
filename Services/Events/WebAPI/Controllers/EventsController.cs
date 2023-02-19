using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IUserEventService _service;
        private readonly IMapper _mapper;

        public EventsController(IUserEventService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userEvents = await _service.GetUserEvents();
            var dtos = _mapper.Map<IEnumerable<UserEventDto>>(userEvents);
            return Ok(dtos);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UserEventDto userEventDto)
        {
            var userEvent = await _service.AddNewUserEvent(_mapper.Map<UserEvent>(userEventDto), _mapper.Map<RecurrencyRule>(userEventDto.RecurrencyRule));
            var dto = _mapper.Map<UserEventDto>(userEvent);
            return Ok(dto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userEvent = await _service.GetUserEventById(id);
            var dto = _mapper.Map<UserEventDto>(userEvent);
            return Ok(dto);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UserEventDto userEventDto)
        {
            var userEvent = await _service.UpdateUserEvent(_mapper.Map<UserEvent>(userEventDto), _mapper.Map<RecurrencyRule>(userEventDto.RecurrencyRule));
            var dto = _mapper.Map<UserEventDto>(userEvent);
            return Ok(dto);
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _service.RemoveUserEvent(id);            
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EventNames([FromBody] IEnumerable<Guid> eventIds)
        {
            var dict = await _service.GetEventNames(eventIds);
            return Ok(dict);
        }
    }
}
