using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IEventsClient _eventsClient;        
        private readonly IMapper _mapper;        

        public ActivitiesController(IEventsClient eventsClient, IMapper mapper)
        {
            _eventsClient = eventsClient;            
            _mapper = mapper;            
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Activities()
        {
            // Get all activities from for example EventService
            // Events and Subscription share one mongo database
            var records = await _eventsClient.GetAllActivities();                                     
            return View(_mapper.Map<IEnumerable<ActivitiesOverviewViewModel>>(records.OrderByDescending(x => x.TimeOfAction)));
        }
    }
}
