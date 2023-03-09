using ApplicationCore.Models.Enums;
using MediatR;

namespace ApplicationCore.Models.Notifications
{
    public abstract class BaseNotification : INotification
    {
        public BaseNotification(UserActionOnEvent userActionOnEvent, string userName, string synopsis)
        {
            UserActionOnEvent = userActionOnEvent;
            Synopsis = synopsis;            
            UserName = userName;
            TimeOfAction = DateTime.Now;
        }

        public abstract UserActionOnEvent UserActionOnEvent { get; init; }
        public string Synopsis { get; init; } = string.Empty;
        public DateTime TimeOfAction { get; }
        public string UserName { get; init; } = string.Empty;
    }
}