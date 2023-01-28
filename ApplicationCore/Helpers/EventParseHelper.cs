using ApplicationCore.Models;
using ApplicationCore.Models.Entities;
using System.Text.Json;

namespace ApplicationCore.Helpers
{
    public static class EventParseHelper
    {
        public static string SerializeUserEvensToJsonString(IEnumerable<UserEvent> userEvents)
        {
            var calendarEvents = new List<CalendarEvent>(userEvents.Count());
            foreach (var (userEvent, i) in userEvents.Select((ue, i) => (ue, i)))
            {
                calendarEvents.Add(new CalendarEvent(
                    id: i + 1,
                    start: userEvent.Date,
                    end: userEvent.Date,
                    startTime: userEvent.StartTime,
                    endTime: userEvent.EndTime,
                    title: userEvent.Name));
            }
            return JsonSerializer.Serialize(calendarEvents);
        }
    }
}
