using ApplicationCore.Models.Entities;
using AutoMapper;
using WebUI.Models.Dtos;

namespace ApplicationCore.Profiles
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, GetInstructorDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));               
        }
    }
}
