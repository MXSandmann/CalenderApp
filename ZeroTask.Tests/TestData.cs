using ZeroTask.DAL.Entities;

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
                    Id = 1,
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
                    Id = 2,
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
    }
}
