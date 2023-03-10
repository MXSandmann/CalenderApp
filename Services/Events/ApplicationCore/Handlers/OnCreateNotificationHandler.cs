using ApplicationCore.Models.Documents;
using ApplicationCore.Models.Notifications;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApplicationCore.Handlers
{
    public class OnCreateNotificationHandler : INotificationHandler<OnCreateNotification>
    {
        private readonly ILogger<OnCreateNotificationHandler> _logger;
        private readonly IUserActivityService _userActivityService;
        private readonly IMapper _mapper;

        public OnCreateNotificationHandler(ILogger<OnCreateNotificationHandler> logger, IUserActivityService userActivityService, IMapper mapper)
        {
            _logger = logger;
            _userActivityService = userActivityService;
            _mapper = mapper;
        }

        public async Task Handle(OnCreateNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--> Here should be mongo db record creation for record: {record}", JsonConvert.SerializeObject(notification));
            var record = _mapper.Map<UserActivityRecord>(notification);
            await _userActivityService.Create(record);
        }
    }
}
