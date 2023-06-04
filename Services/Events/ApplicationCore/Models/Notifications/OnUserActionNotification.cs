using ApplicationCore.Models.Enums;
using MediatR;

namespace ApplicationCore.Models.Notifications
{
    public record OnUserActionNotification(UserActionOnEvent UserActionOnEvent, string UserName, string Synopsis) : INotification
    {        
        public DateTime TimeOfAction { get; } = DateTime.Now;     
    }
}