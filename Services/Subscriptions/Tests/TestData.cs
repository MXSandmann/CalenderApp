using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;

namespace Tests
{
    internal static class TestData
    {
        internal static IEnumerable<Subscription> GetSubscriptions()
        {
            return new List<Subscription>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserEmail = "dfsd@fds",
                    UserName = "Test",
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserEmail = "dasasfsd@fds",
                    UserName = "Test",
                }
            };
        }
        internal static NotificationDto GetNotificationDto()
        {
            var dateTime = new DateTime(2023, 6, 1, 12, 0, 0);
            return new NotificationDto
            {
                NotificationTime = dateTime,                
                SubscriptionId = Guid.NewGuid(),
            };
        }

        internal static Subscription GetSubscription()
        {
            return new Subscription
            {
                Id = Guid.NewGuid(),
                EventId = Guid.NewGuid(),
                UserEmail = "dnfkjsnd@dfsd",
                UserName = "Test",
            };
        }
    }
}
