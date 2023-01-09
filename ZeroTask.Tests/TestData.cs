using ZeroTask.DAL.Entities;
using ZeroTask.PL.Models;

namespace ZeroTask.Tests
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
                    Date = DateTime.Today,
                    Time = DateTime.Now,
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
                    Date = DateTime.Today,
                    Time = DateTime.Now,
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
                Date = DateTime.Today,
                Time = DateTime.Now,
                Description = "testdescr3",
                AdditionalInfo = "addinfo3",
                ImageUrl = "testurl3"
            };
        }
    }
}
