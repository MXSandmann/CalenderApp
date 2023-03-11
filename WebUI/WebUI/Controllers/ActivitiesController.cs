using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;

namespace WebUI.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IEventsClient _eventsClient;
        private readonly ISubscriptionsClient _subscriptionsClient;

        public ActivitiesController(IEventsClient eventsClient, ISubscriptionsClient subscriptionsClient)
        {
            _eventsClient = eventsClient;
            _subscriptionsClient = subscriptionsClient;
        }

        [HttpGet("[action]")]        
        public async Task<IActionResult> Activities()
        {
            var taskEvents = _eventsClient.GetAllActivities();
            var taskSubscriptions = _subscriptionsClient.GetAllActivities();
            await Task.WhenAll(taskEvents, taskSubscriptions);

            var activitiesEvents = await taskEvents;
            var activitiesSubscriptions = await taskSubscriptions;
            activitiesEvents.ToList().AddRange(activitiesSubscriptions);

            return View(activitiesEvents);
        }
    }
}
