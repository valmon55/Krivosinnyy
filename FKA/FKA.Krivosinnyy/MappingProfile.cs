using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
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

            CreateMap<LoginViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.PasswordHash, opt => opt.MapFrom(c => c.Password));
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.First_Name, opt => opt.MapFrom(c => c.First_Name))
                .ForMember(x => x.Last_Name, opt => opt.MapFrom(c => c.Last_Name))
                .ForMember(x => x.Middle_Name, opt => opt.MapFrom(c => c.Middle_Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.Day, opt => opt.MapFrom(c => c.BirthDate.Day))
                .ForMember(x => x.Month, opt => opt.MapFrom(c => c.BirthDate.Month))
                .ForMember(x => x.Year, opt => opt.MapFrom(c => c.BirthDate.Year))
                .ForMember(x => x.Login, opt => opt.MapFrom(c => c.UserName));
            CreateMap<PersonViewModel, Person>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(c => c.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(c => c.LastName))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Day)));
            CreateMap<Person, PersonViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.Photo, opt => opt.MapFrom(c => c.Avatar.Path))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(c => c.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(c => c.LastName))
                .ForMember(x => x.MiddleName, opt => opt.MapFrom(c => c.MiddleName))
                .ForMember(x => x.Day, opt => opt.MapFrom(c => c.BirthDate.Day))
                .ForMember(x => x.Month, opt => opt.MapFrom(c => c.BirthDate.Month))
                .ForMember(x => x.Year, opt => opt.MapFrom(c => c.BirthDate.Year));
        }
    }
}
