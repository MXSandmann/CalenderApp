using ApplicationCore.FileGenerators.Contracts;
using ApplicationCore.Models.Entities;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using CalendarEvent = Ical.Net.CalendarComponents.CalendarEvent;

namespace ApplicationCore.FileGenerators
{
    public class IcsFileGenerator : IIcsFileGenerator
    {
        public string Generate(UserEvent userEvent)
        {
            // Create a new calendar
            var calendar = new Calendar();

            // Create a new calendar event
            var calendarEvent = new CalendarEvent
            {
                Name = userEvent.Name,
                Summary = userEvent.Description,
                Location = userEvent.Place,
                Start = new CalDateTime(userEvent.Date.Add(userEvent.StartTime.TimeOfDay)),
                End = new CalDateTime(userEvent.LastDate.Add(userEvent.StartTime.TimeOfDay)),
                Uid = userEvent.Id.ToString(),                
            };

            // Add the event to the calendar
            calendar.Events.Add(calendarEvent);

            // Serialize the calendar
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);

            return serializedCalendar;
        }
    }
}
