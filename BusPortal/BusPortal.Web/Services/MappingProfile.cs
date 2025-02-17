using AutoMapper;
using BusPortal.DAL.Persistence.Entities;
namespace BusPortal.Web.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
            CreateMap<BusPortal.Common.Models.RegisterViewModel, BusPortal.Web.Models.DTO.RegisterViewModel>();
            CreateMap<BusPortal.Web.Models.DTO.RegisterViewModel, BusPortal.Common.Models.RegisterViewModel>();
            CreateMap<BusPortal.Common.Models.LoginViewModel, BusPortal.Web.Models.DTO.LoginViewModel>();
            CreateMap<BusPortal.Web.Models.DTO.LoginViewModel, BusPortal.Common.Models.LoginViewModel>();

           
            CreateMap<Common.Models.RegisterViewModel, Client>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => false)); 
        }
    }
}
