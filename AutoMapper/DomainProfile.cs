using app_test_jmeter.Models;
using app_test_jmeter.Models.ViewModels;
using AutoMapper;

namespace app_test_jmeter.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(d => d.Filename, opt => opt.MapFrom(src => src.PerfilImage))
                .ForMember(d => d.PerfilImage, opt => opt.Ignore());

            CreateMap<UserViewModel,User>()
                .ForMember(d => d.PerfilImage, opt => opt.MapFrom(src => src.PerfilImage.FileName));
        }
    }
}