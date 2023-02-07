using ApplicationCore.Models;
using ApplicationCore.Models.Entities;
using ApplicationCore.Models.Enums;
using AutoMapper;
using WebUI.Models;

namespace WebUI.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserEvent, GetUserEventViewModel>()
                .ForMember(dest => dest.Recurrency, opt => opt
                .MapFrom(src => GetUserEventViewModel.GetRecurrencyDescription(src.RecurrencyRule!)));

            CreateMap<UserEvent, CalendarEvent>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.Date));

            CreateMap<UserEvent, CreateUpdateUserEventViewModel>()
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

            CreateMap<CreateUpdateUserEventViewModel, UserEvent>()
                .ForMember(dest => dest.LastDate, opt => opt.MapFrom(src => src.LastDate == null ? src.Date : src.LastDate));

            CreateMap<CreateUpdateUserEventViewModel, RecurrencyRule>()
                .ForMember(dest => dest.UserEventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Recurrency, opt => opt.MapFrom(src => src.Recurrency != null ? src.Recurrency : default))
                .ForMember(dest => dest.WeekOfMonth, opt => opt.MapFrom(src => src.WeekOfMonth != null ? src.WeekOfMonth : default))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => ComputeCertainDays(src)));
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
