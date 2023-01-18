using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class CalendarEvent
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
