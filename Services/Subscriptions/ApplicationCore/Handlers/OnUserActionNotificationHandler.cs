using ApplicationCore.Models.Documents;
using ApplicationCore.Models.Notifications;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using MediatR;

namespace ApplicationCore.Handlers
{
    public class OnUserActionNotificationHandler : INotificationHandler<OnUserActionNotification>
    {
        private readonly IUserActivityService _userActivityService;
        private readonly IMapper _mapper;

        public OnUserActionNotificationHandler(IUserActivityService userActivityService, IMapper mapper)
        {
            _userActivityService = userActivityService;
            _mapper = mapper;
        }

        public async Task Handle(OnUserActionNotification notification, CancellationToken cancellationToken)
        {
            var record = _mapper.Map<UserActivityRecord>(notification);
            await _userActivityService.Create(record);
        }
    }
}
