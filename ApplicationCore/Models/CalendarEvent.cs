using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CalendarEvent
    {
        public CalendarEvent(int id, string title, DateTime start, DateTime end)
        {
            this.id = id;
            this.title = title;
            this.start = start;
            this.end = end;
        }
        public int id { get; }
        public string title { get; }
        public DateTime start { get; }
        public DateTime end { get; }
    }
}
