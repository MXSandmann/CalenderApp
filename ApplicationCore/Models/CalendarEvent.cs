using ApplicationCore.Models.Entities;

namespace ApplicationCore.Models
{
    public class CalendarEvent
    {
        public CalendarEvent(string title, DateTime start, DateTime end, DateTime startTime, DateTime endTime)
        {
            id = Guid.NewGuid().ToString();
            this.title = title;
            this.start = start;
            this.end = end;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public string id { get; }
        public string title { get; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public DateTime startTime { get; }
        public DateTime endTime { get; }
        
        public static CalendarEvent Copy(CalendarEvent ce)
        {
            return new CalendarEvent(ce.title, ce.start, ce.end, ce.startTime, ce.endTime);
        }

        public static CalendarEvent ToCalendarEvent(UserEvent ue)
        {
            return new CalendarEvent(
                    ue.Name,
                    ue.Date,
                    ue.Date,
                    ue.StartTime,
                    ue.EndTime);
        }
    }
}
