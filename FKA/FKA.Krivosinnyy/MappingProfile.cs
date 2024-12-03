using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Day)))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        }
    }
}
