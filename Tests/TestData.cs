using ApplicationCore.Models;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using WebUI.Models;

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
                    HasRecurrency = YesNo.No
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
                    HasRecurrency = YesNo.No
                }
            };
        }

        internal static CreateUpdateUserEventViewModel GetUserEventViewModel()
        {
            return new CreateUpdateUserEventViewModel
            {
                Id = Guid.NewGuid(),
                Name = "testname3",
                Category = "testcategory3",
                Place = "testplace3",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Date = DateTime.Now,
                LastDate = DateTime.Now,
                Description = "testdescr3",
                AdditionalInfo = "addinfo3",
                ImageUrl = "testurl3",
                HasRecurrency = YesNo.No
            };
        }

        internal static CreateUpdateUserEventViewModel GetInvalidUserEventViewModel()
        {
            // Start time < End time
            return new CreateUpdateUserEventViewModel
            {
                Id = Guid.NewGuid(),
                Name = "testname3",
                Category = "testcategory3",
                Place = "testplace3",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(-1),
                Date = DateTime.Now,
                LastDate = DateTime.Now,
                Description = "testdescr3",
                AdditionalInfo = "addinfo3",
                ImageUrl = "testurl3",
                HasRecurrency = YesNo.No
            };
        }

        internal static RecurrencyRule GetRecurrencyRule(Recurrency recurrency)
        {
            return new RecurrencyRule
            {
                Id = Guid.NewGuid(),
                Recurrency = recurrency,
                CertainDays = 1,
                WeekOfMonth = WeekOfTheMonth.None,
                EvenOdd = EvenOdd.None
            };
        }
                

        internal static IEnumerable<CalendarEvent> GetCalendarEvents()
        {
            return new List<CalendarEvent>
            {
                new CalendarEvent("testname1",
                new DateTime(2023, 6, 1, 12, 0, 0),
                new DateTime(2023, 6, 1, 12, 0, 0),
                new DateTime(2023, 6, 1, 12, 0, 0),
                new DateTime(2023, 6, 1, 13, 0, 0)),
                new CalendarEvent("testname2",
                new DateTime(2023, 7, 1, 12, 0, 0),
                new DateTime(2023, 7, 1, 12, 0, 0),
                new DateTime(2023, 7, 1, 12, 0, 0),
                new DateTime(2023, 7, 1, 13, 0, 0)),
            };
        }
    }
}
