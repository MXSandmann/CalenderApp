using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constants
{
    public static class DaysAsBinary
    {
        public const byte EMPTY = 0b0000_0000;
        public const byte WEEKEND = 0b0110_0000;
        public const byte WORKINGDAYS = 0b0001_1111;

        public const byte MONDAY = 0b0000_0001;
        public const byte TUESDAY = 0b0000_0010;
        public const byte WEDNESDAY = 0b0000_0100;
        public const byte THURSDAY = 0b0000_1000;
        public const byte FRIDAY = 0b0001_0000;
        public const byte SATURDAY = 0b0010_0000;
        public const byte SUNDAY = 0b0100_0000;

    }
}
