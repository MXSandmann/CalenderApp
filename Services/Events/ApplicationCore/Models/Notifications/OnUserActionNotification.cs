using ApplicationCore.Models.Enums;
using MediatR;

namespace ApplicationCore.Models.Notifications
{
    public record OnUserActionNotification(UserActionOnEvent UserActionOnEvent, string UserName, string Synopsis) : INotification
    {
        //public OnUserActionNotification(UserActionOnEvent userActionOnEvent, string userName, string synopsis)
        //{
        //    UserActionOnEvent = userActionOnEvent;
        //    Synopsis = synopsis;
        //    UserName = userName;
        //    TimeOfAction = DateTime.Now;
        //}

        //public UserActionOnEvent UserActionOnEvent { get; init; }
        //public string Synopsis { get; init; } = string.Empty;
        public DateTime TimeOfAction { get; } = DateTime.Now;
        //public string UserName { get; init; } = string.Empty;
    }
}