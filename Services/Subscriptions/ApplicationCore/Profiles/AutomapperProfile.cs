using ApplicationCore.Models.Documents;
using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Notifications;
using AutoMapper;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<OnUserActionNotification, UserActivityRecord>()
                .ForMember(dest => dest.UserAction, opt => opt.MapFrom(src => src.UserActionOnEvent.ToString()));

            CreateMap<UserActivityRecord, UserActivityRecordDto>();
        }
    }
}
