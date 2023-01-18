using ApplicationCore.Models;
using System.Text.Json;

namespace ApplicationCore.Helpers
{
    public static class JsonHelper
    {
        public static string SerializeUserEvensToJsonString(IEnumerable<UserEvent> userEvents)
        {
            var calendarEvents = new List<CalendarEvent>(userEvents.Count());
            foreach (var (userEvent, i) in userEvents.Select((v, i) => (v, i)))
            {
                var calendarEvent = new CalendarEvent
                {
                    id = i + 1,
                    start = DateTime.Now,//userEvent.Date,
                    end= DateTime.Now.AddDays(1)           
                };
                calendarEvents.Add(calendarEvent);
            }
            return JsonSerializer.Serialize(calendarEvents);
        }
    }
}
