using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dest => dest.SubscriptionId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }   
    }
}
