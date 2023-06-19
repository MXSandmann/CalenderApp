using ApplicationCore.Models.Dto;
using ApplicationCore.Models.Entities;
using AutoMapper;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<InvitationDto, Invitation>()
                .ForMember(dest => dest.InviterUserName, opt => opt.MapFrom(src => src.UserName));
            CreateMap<Invitation, InvitationDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.InviterUserName));
        }
    }
}
