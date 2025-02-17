using AutoMapper;
using BusPortal.Common.Models;
using BusPortal.BLL.Domain.Models; 
namespace BusPortal.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map RegisterViewModel tek Client
            CreateMap<RegisterViewModel, Client>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => false)); //Admini esht false si default
        }
    }
}
