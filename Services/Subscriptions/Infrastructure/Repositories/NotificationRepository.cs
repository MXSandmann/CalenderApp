using ApplicationCore.Models.Entities;
using ApplicationCore.Repositories.Contracts;
using Infrastructure.DataContext;

namespace Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly SubscriptionDataContext _context;

        public NotificationRepository(SubscriptionDataContext context)
        {
            _context = context;
        }

        public async Task<Notification> Add(Notification notification)
        {
            await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }
    }
}
