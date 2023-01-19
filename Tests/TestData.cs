using ApplicationCore.Models;
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
                    ImageUrl = "testurl1"
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
                    ImageUrl = "testurl2"
                }
            };
        }

        internal static UserEventViewModel GetUserEventViewModel()
        {
            return new UserEventViewModel
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
                ImageUrl = "testurl3"
            };
        }
    }
}
