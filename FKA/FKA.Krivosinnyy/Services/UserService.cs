using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services.IServices;

namespace FKA.Krivosinnyy.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public List<UserViewModel> AllUsers()
        {
            var users = _userRepository.GetAll();
            var usersView = new List<UserViewModel>();

            foreach(var user in users)
            {
                var userView = _mapper.Map<UserViewModel>(user);
                usersView.Add(userView);
            }
            return usersView;
        }

        public void DeleteUser(uint userId)
        {
            throw new NotImplementedException();
        }

        public UserViewModel UpdateUser(uint userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
