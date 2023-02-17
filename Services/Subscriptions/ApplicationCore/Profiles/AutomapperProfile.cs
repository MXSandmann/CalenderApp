using ApplicationCore.Models.Dtos;
using ApplicationCore.Models.Entities;
using AutoMapper;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
        }   
    }
}
