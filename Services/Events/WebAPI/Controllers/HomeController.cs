using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserEventService _service;        

        public HomeController(IUserEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var calendarEvents = await _service.GetCalendarEvents();
            return Ok(calendarEvents);
        }
    }
}
