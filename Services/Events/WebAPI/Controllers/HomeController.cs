﻿using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTelemetry;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IUserEventService _service;
        private readonly ActivitySource _activitySource;

        public HomeController(IUserEventService service, ActivitySource activitySource)
        {
            _service = service;
            _activitySource = activitySource;
        }

        [HttpGet("{userId:guid?}")]
        public async Task<IActionResult> Index([FromRoute] Guid? userId)
        {
            using var activity = _activitySource.StartActivity("HomeController Activity");
            var calendarEvents = await _service.GetCalendarEvents(userId.GetValueOrDefault());
            Baggage.SetBaggage("calendar_events", JsonConvert.SerializeObject(calendarEvents));
            return Ok(calendarEvents);
        }
    }
}
