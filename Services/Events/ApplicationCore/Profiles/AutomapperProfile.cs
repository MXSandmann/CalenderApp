using ApplicationCore.Models;
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

            CreateMap<UserEvent, CalendarEvent>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.Date));
            CreateMap<UserEvent, UserEventDto>();
            CreateMap<UserEventDto, UserEvent>()
                .ForMember(dest => dest.RecurrencyRule, opt => opt.Ignore());
            CreateMap<RecurrencyRule, RecurrencyRuleDto>().ReverseMap();

            CreateMap<OnUserActionNotification, UserActivityRecord>()
                .ForMember(dest => dest.UserAction, opt => opt.MapFrom(src => src.UserActionOnEvent.ToString()));

            CreateMap<UserActivityRecord, UserActivityRecordDto>();
        }
    }
}
