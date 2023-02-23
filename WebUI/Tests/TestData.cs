using WebUI.Models;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;

namespace Tests
{
    internal static class TestData
    {
        internal static NotificationDto GetNotificationDto()
        {
            var dateTime = new DateTime(2023, 6, 1, 12, 0, 0);
            return new NotificationDto
            {
                NotificationTime = dateTime,
                EventName = "Test",
                SubscriptionId = Guid.NewGuid(),
            };
        }
        internal static IEnumerable<SubscriptionDto> GetSubscriptionDtos()
        {
            var dateTime = new DateTime(2023, 6, 1, 12, 0, 0);
            return new List<SubscriptionDto>
            {
                new()
                {
                    EventId = Guid.NewGuid(),
                    EventName = "Ski jumping",
                    UserEmail = "mda@gfg",
                    UserName = "Mjuj",
                    SubscriptionId = Guid.NewGuid(),
                },
                new()
                {
                    EventId = Guid.NewGuid(),
                    EventName = "Music lesson",
                    UserEmail = "mda@gfg",
                    UserName = "Mjuj",
                    SubscriptionId = Guid.NewGuid(),
                    Notifications = new List<NotificationDto>
                    {
                        new() { NotificationTime = dateTime },
                        new() { NotificationTime = dateTime.AddHours(1) },
                    }
                }
            };
        }

        internal static SubscriptionDto GetSubscriptionDtoWithGivenEventId(Guid id)
        {
            
            return new SubscriptionDto
            {            
                EventId = id,                
                UserEmail = "mda@gfg",
                UserName = "Mjuj",
                SubscriptionId = Guid.NewGuid()
            };
        }

        internal static IEnumerable<UserEventDto> GetUserEventDtos()
        {
            var dateTime = new DateTime(2023, 6, 1, 12, 0, 0);
            return new List<UserEventDto>(2)
            {
                new UserEventDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testname1",
                    Category = "b",
                    Place = "d",
                    StartTime = dateTime,
                    EndTime = dateTime.AddHours(1),
                    Date = dateTime,
                    LastDate= dateTime,
                    Description = "testdescr1",
                    AdditionalInfo = "addinfo1",
                    ImageUrl = "testurl1",
                    HasRecurrency = false
                },
                new UserEventDto
                {
                    Id = Guid.NewGuid(),
                    Name = "testname2",
                    Category = "a",
                    Place = "c",
                    StartTime = dateTime.AddHours(3),
                    EndTime = dateTime.AddHours(4),
                    Date = dateTime.AddDays(1),
                    LastDate = dateTime.AddDays(2),
                    Description = "testdescr2",
                    AdditionalInfo = "addinfo2",
                    ImageUrl = "testurl2",
                    HasRecurrency = false
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
                HasRecurrency = false
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
                HasRecurrency = false
            };
        }

        internal static RecurrencyRuleDto GetRecurrencyRuleDto(Recurrency recurrency)
        {
            return new RecurrencyRuleDto
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
