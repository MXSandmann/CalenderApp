﻿using ApplicationCore.Models.Entities;
using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    public class CalendarEvent : ICloneable
    {
        [JsonProperty("id")]
        public string Id { get; } = Guid.NewGuid().ToString();
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("startTime")]
        public DateTime StartTime { get; set; }
        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        //public static CalendarEvent ToCalendarEvent(UserEvent userEvent)
        //{
        //    return new CalendarEvent(
        //            userEvent.Name,
        //            userEvent.Date,
        //            userEvent.Date,
        //            userEvent.StartTime,
        //            userEvent.EndTime);
        //}

        public object Clone()
        {
            return new CalendarEvent
            {
                Title = Title,
                Start = Start,
                End = End,
                StartTime = StartTime,
                EndTime = EndTime
            };
        }
    }
}
