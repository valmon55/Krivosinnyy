using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services.IServices;

namespace FKA.Krivosinnyy.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public PersonService(IUserRepository userRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public void AddPerson(PersonViewModel person)
        {
            throw new NotImplementedException();
        }
        public List<PersonViewModel> AllPersons()
        {
            var users = _userRepository.GetAll();
            var usersView = new List<PersonViewModel>();

            foreach(var user in users)
            {
                var userView = _mapper.Map<PersonViewModel>(user);
                usersView.Add(userView);
            }
            return usersView;
        }
        public PersonViewModel UpdatePerson(int personId)
        {
            throw new NotImplementedException();
        }
        public void UpdatePerson(PersonViewModel person)
        {
            throw new NotImplementedException();
        }
        public void DeletePerson(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
