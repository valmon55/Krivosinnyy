using AutoMapper;
using FKA.Krivosinnyy.BLL.Extentions;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
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
        public UserViewModel GetUser(UInt32 userId)
        {
            var user = _userRepository.Get(userId);
            return _mapper.Map<UserViewModel>(user);
        }
        public void DeleteUser(UInt32 userId)
        {
            _userRepository.Delete(_userRepository.Get(userId));
        }
        public UserViewModel UpdateUser(UInt32 userId)
        {
            throw new NotImplementedException();
        }
        public void UpdateUser(UserViewModel userView)
        {
            var user = _userRepository.Get(userView.Id);
            user.Convert(userView);
            _userRepository.Update(user);
        }
    }
}
