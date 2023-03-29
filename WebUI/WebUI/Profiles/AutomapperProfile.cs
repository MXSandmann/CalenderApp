using AutoMapper;
using WebUI.Models;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;
using WebUI.Models.ViewModels;

namespace WebUI.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserEventDto, GetUserEventViewModel>()
                .ForMember(dest => dest.Recurrency, opt => opt
                .MapFrom(src => GetUserEventViewModel.GetRecurrencyDescription(src.RecurrencyRule!)));

            CreateMap<UserEventDto, CalendarEvent>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.Date));

            CreateMap<UserEventDto, CreateUpdateUserEventViewModel>()
                .ForMember(dest => dest.Recurrency, opt => opt.MapFrom(src => src.RecurrencyRule != null ? src.RecurrencyRule.Recurrency : default))
                .ForMember(dest => dest.WeekOfMonth, opt => opt.MapFrom(src => src.RecurrencyRule != null ? src.RecurrencyRule.WeekOfMonth : default))
                .ForMember(dest => dest.OnMonday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                            && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Monday)))
                .ForMember(dest => dest.OnTuesday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                             && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Tuesday)))
                .ForMember(dest => dest.OnWednesday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                               && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Wednesday)))
                .ForMember(dest => dest.OnThursday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                              && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Thursday)))
                .ForMember(dest => dest.OnFriday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                            && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Friday)))
                .ForMember(dest => dest.OnSaturday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                              && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Saturday)))
                .ForMember(dest => dest.OnSunday, opt => opt.MapFrom(src => src.RecurrencyRule != null
                                                                            && src.RecurrencyRule.DayOfWeek.HasFlag(CertainDays.Sunday)))
                .ForMember(dest => dest.EvenOdd, opt => opt.MapFrom(src => src.RecurrencyRule != null ? src.RecurrencyRule.EvenOdd : default));

            CreateMap<CreateUpdateUserEventViewModel, UserEventDto>()
                .ForMember(dest => dest.LastDate, opt => opt.MapFrom(src => src.LastDate == null ? src.Date : src.LastDate));

            CreateMap<CreateUpdateUserEventViewModel, RecurrencyRuleDto>()
                .ForMember(dest => dest.UserEventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Recurrency, opt => opt.MapFrom(src => src.Recurrency != null ? src.Recurrency : default))
                .ForMember(dest => dest.WeekOfMonth, opt => opt.MapFrom(src => src.WeekOfMonth != null ? src.WeekOfMonth : default))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => ComputeCertainDays(src)));

            CreateMap<RecurrencyRuleDto, CreateUpdateUserEventViewModel>()
                .ForMember(dest => dest.OnWeekend, opt => opt.MapFrom(src => CreateUpdateUserEventViewModel.IsOnWeekend(src.DayOfWeek)))
                .ForMember(dest => dest.OnWorkingDays, opt => opt.MapFrom(src => CreateUpdateUserEventViewModel.IsOnWorkingDays(src.DayOfWeek)));

            CreateMap<CreateSubscriptionViewModel, SubscriptionDto>().ReverseMap();
            CreateMap<CreateNotificationViewModel, NotificationDto>();
            CreateMap<SubscriptionDto, GetSubscriptionViewModel>()
                .ForMember(dest => dest.Notifications, opt => opt.MapFrom(src => GetSubscriptionViewModel.NotificationsToString(src.Notifications!)));

            CreateMap<SmartSearchViewModel, SearchUserEventsDto>().ReverseMap();

            CreateMap<UserActivityRecordDto, ActivitiesOverviewViewModel>();

            CreateMap<UserDto, LoginViewModel>().ReverseMap();

            CreateMap<RegisterViewModel, UserRegistrationDto>();
        }

        private static CertainDays ComputeCertainDays(CreateUpdateUserEventViewModel model)
        {
            var certaindays = (model.OnMonday || model.OnWorkingDays) ? CertainDays.Monday : 0;
            certaindays |= (model.OnTuesday || model.OnWorkingDays) ? CertainDays.Tuesday : 0;
            certaindays |= (model.OnWednesday || model.OnWorkingDays) ? CertainDays.Wednesday : 0;
            certaindays |= (model.OnThursday || model.OnWorkingDays) ? CertainDays.Thursday : 0;
            certaindays |= (model.OnFriday || model.OnWorkingDays) ? CertainDays.Friday : 0;
            certaindays |= (model.OnSaturday || model.OnWeekend) ? CertainDays.Saturday : 0;
            certaindays |= (model.OnSunday || model.OnWeekend) ? CertainDays.Sunday : 0;
            return certaindays;
        }
    }
}
