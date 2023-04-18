using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        public readonly IUserActivityService _userActivityService;
        private readonly ActivitySource _activitySource;

        public ActivitiesController(IUserActivityService userActivityService, ActivitySource activitySource)
        {
            _userActivityService = userActivityService;
            _activitySource = activitySource;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var activity = _activitySource.StartActivity($"{nameof(ActivitiesController)}: {nameof(Get)} action");
            var activities = await _userActivityService.GetAll();
            return Ok(activities);
        }
    }
}
