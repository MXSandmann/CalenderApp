using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        public readonly IUserActivityService _userActivityService;

        public ActivitiesController(IUserActivityService userActivityService)
        {
            _userActivityService = userActivityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var activities = await _userActivityService.GetAll();
            return Ok(activities);
        }
    }
}
