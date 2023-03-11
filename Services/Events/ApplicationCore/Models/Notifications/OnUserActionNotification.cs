using ApplicationCore.Models.Enums;
using MediatR;

namespace ApplicationCore.Models.Notifications
{
    public class OnUserActionNotification : INotification
    {
        public OnUserActionNotification(UserActionOnEvent userActionOnEvent, string userName, string synopsis)
        {
            UserActionOnEvent = userActionOnEvent;
            Synopsis = synopsis;
            UserName = userName;
            TimeOfAction = DateTime.Now;
        }

        public UserActionOnEvent UserActionOnEvent { get; init; }
        public string Synopsis { get; init; } = string.Empty;
        public DateTime TimeOfAction { get; }
        public string UserName { get; init; } = string.Empty;
    }
}