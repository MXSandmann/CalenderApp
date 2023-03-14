using ApplicationCore.Models.Entities;

namespace ApplicationCore.Repositories.Contracts
{
    public interface INotificationRepository
    {
        Task<Notification> Add(Notification notification);
    }
}
