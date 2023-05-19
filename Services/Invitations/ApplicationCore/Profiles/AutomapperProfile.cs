using ApplicationCore.Models.Dto;
using ApplicationCore.Models.Entities;
using AutoMapper;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<InvitationDto, Invitation>().ReverseMap();
        }
    }
}
