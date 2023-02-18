using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> Add(Notification notification);
    }
}
