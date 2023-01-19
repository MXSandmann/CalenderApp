using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CalendarEvent
    {
        public CalendarEvent(int id, string title, DateTime start, DateTime end, DateTime startTime, DateTime endTime)
        {
            this.id = id;
            this.title = title;
            this.start = start;
            this.end = end;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        public int id { get; }
        public string title { get; }
        public DateTime start { get; }
        public DateTime end { get; }
        public DateTime startTime { get; }
        public DateTime endTime { get; }
    }
}
