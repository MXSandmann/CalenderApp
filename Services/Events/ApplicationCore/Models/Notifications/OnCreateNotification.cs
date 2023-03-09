using ApplicationCore.Models.Enums;

namespace ApplicationCore.Models.Notifications
{
    public class OnCreateNotification : BaseNotification
    {
        public OnCreateNotification(UserActionOnEvent userActionOnEvent, string userName, string synopsis) : base(userActionOnEvent, userName, synopsis)
        {
        }

        public override UserActionOnEvent UserActionOnEvent { get; init; }
    }
}
