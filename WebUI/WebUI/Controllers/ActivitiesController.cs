using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;

namespace WebUI.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IEventsClient _eventsClient;

        public ActivitiesController(IEventsClient eventsClient)
        {
            _eventsClient = eventsClient;
        }

        [HttpGet("[action]")]        
        public async Task<IActionResult> Activities()
        {
            var activitiesEvents = await _eventsClient.GetAllActivities();
            return View(activitiesEvents);
        }
    }
}
