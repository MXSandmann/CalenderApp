using Amazon.Runtime.SharedInterfaces;
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
        public string GenerateFromSingleEvent(UserEvent userEvent)
        {
            return Generate(new List<UserEvent>(1) { userEvent });
        }
        public string GenerateFromManyEvents(IEnumerable<UserEvent> userEvents)
        {
            return Generate(userEvents);
        }

        private static string Generate(IEnumerable<UserEvent> userEvents)
        {
            // Create a new calendar
            var calendar = new Calendar();

            foreach (var userEvent in userEvents)
            {
                // Create a new calendar event
                var calendarEvent = new CalendarEvent
                {
                    Name = userEvent.Name,
                    Summary = userEvent.Description,
                    Location = userEvent.Place,
                    Start = new CalDateTime(userEvent.Date.Add(userEvent.StartTime.TimeOfDay)),
                    End = new CalDateTime(userEvent.LastDate.Add(userEvent.StartTime.TimeOfDay)),
                    Uid = userEvent.Id.ToString()
                };

                // Add the event to the calendar
                calendar.Events.Add(calendarEvent);
            }

            // Serialize the calendar
            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);

            return serializedCalendar;
        }
    }
}
