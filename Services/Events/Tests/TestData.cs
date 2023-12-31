﻿using ApplicationCore.Models;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;

namespace Tests
{
    internal static class TestData
    {
        internal static IEnumerable<UserEvent> GetUserEvents()
        {
            return new List<UserEvent>(2)
            {
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "testname1",
                    Category = "testcategory1",
                    Place = "testplace1",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    Date = DateTime.Now,
                    LastDate= DateTime.Now,
                    Description = "testdescr1",
                    AdditionalInfo = "addinfo1",
                    ImageUrl = "testurl1",
                    HasRecurrency = false
                },
                new UserEvent
                {
                    Id = Guid.NewGuid(),
                    Name = "testname2",
                    Category = "testcategory2",
                    Place = "testplace2",
                    StartTime = DateTime.Now.AddHours(3),
                    EndTime = DateTime.Now.AddHours(4),
                    Date = DateTime.Now.AddDays(1),
                    LastDate = DateTime.Now.AddDays(2),
                    Description = "testdescr2",
                    AdditionalInfo = "addinfo2",
                    ImageUrl = "testurl2",
                    HasRecurrency = false
                }
            };
        }

        internal static RecurrencyRule GetRecurrencyRule(Recurrency recurrency)
        {
            return new RecurrencyRule
            {
                Id = Guid.NewGuid(),
                Recurrency = recurrency,
                DayOfWeek = (CertainDays)1,
                WeekOfMonth = WeekOfTheMonth.None,
                EvenOdd = EvenOdd.None
            };
        }


        internal static IEnumerable<CalendarEvent> GetCalendarEvents()
        {
            return new List<CalendarEvent>
            {
                new CalendarEvent
                {
                    Title = "testname1",
                    Start = new DateTime(2023, 6, 1, 12, 0, 0),
                    End = new DateTime(2023, 6, 1, 12, 0, 0),
                    StartTime = new DateTime(2023, 6, 1, 12, 0, 0),
                    EndTime = new DateTime(2023, 6, 1, 13, 0, 0)
                },
                new CalendarEvent
                {
                    Title = "testname2",
                    Start = new DateTime(2023, 7, 1, 12, 0, 0),
                    End = new DateTime(2023, 7, 1, 12, 0, 0),
                    StartTime = new DateTime(2023, 7, 1, 12, 0, 0),
                    EndTime = new DateTime(2023, 7, 1, 13, 0, 0)
                }
            };
        }
    }
}
