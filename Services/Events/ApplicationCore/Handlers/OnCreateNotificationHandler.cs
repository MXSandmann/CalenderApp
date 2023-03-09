using ApplicationCore.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.Handlers
{
    public class OnCreateNotificationHandler : INotificationHandler<OnCreateNotification>
    {
        private readonly ILogger<OnCreateNotificationHandler> _logger;

        public OnCreateNotificationHandler(ILogger<OnCreateNotificationHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(OnCreateNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--> Here should be mongo db record creation for record: {record}", JsonConvert.SerializeObject(notification));
            return Task.CompletedTask;
        }
    }
}
