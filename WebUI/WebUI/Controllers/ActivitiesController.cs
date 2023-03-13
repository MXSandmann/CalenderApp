using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IEventsClient _eventsClient;
        private readonly ISubscriptionsClient _subscriptionsClient;
        private readonly IMapper _mapper;

        public ActivitiesController(IEventsClient eventsClient, ISubscriptionsClient subscriptionsClient, IMapper mapper)
        {
            _eventsClient = eventsClient;
            _subscriptionsClient = subscriptionsClient;
            _mapper = mapper;
        }

        [HttpGet("[action]")]        
        public async Task<IActionResult> Activities()
        {
            var taskEvents = _eventsClient.GetAllActivities();
            var taskSubscriptions = _subscriptionsClient.GetAllActivities();
            await Task.WhenAll(taskEvents, taskSubscriptions);

            var activitiesEvents = await taskEvents;
            var activitiesSubscriptions = await taskSubscriptions;
            var joined = activitiesEvents.Concat(activitiesSubscriptions);

            return View(_mapper.Map<IEnumerable<ActivitiesOverviewViewModel>>(joined));
        }
    }
}
