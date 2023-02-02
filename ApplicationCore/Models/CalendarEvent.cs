using ApplicationCore.Models.Entities;
using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    public class CalendarEvent
    {
        public CalendarEvent(string title, DateTime start, DateTime end, DateTime startTime, DateTime endTime)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Start = start;
            End = end;
            StartTime = startTime;
            EndTime = endTime;
        }

        [JsonProperty("id")]
        public string Id { get; }
        [JsonProperty("title")]
        public string Title { get; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("startTime")]
        public DateTime StartTime { get; }
        [JsonProperty("endTime")]
        public DateTime EndTime { get; }

        public static CalendarEvent Copy(CalendarEvent calendarEvent)
        {
            return new CalendarEvent(calendarEvent.Title, calendarEvent.Start, calendarEvent.End, calendarEvent.StartTime, calendarEvent.EndTime);
        }

        public static CalendarEvent ToCalendarEvent(UserEvent userEvent)
        {
            return new CalendarEvent(
                    userEvent.Name,
                    userEvent.Date,
                    userEvent.Date,
                    userEvent.StartTime,
                    userEvent.EndTime);
        }
    }
}
