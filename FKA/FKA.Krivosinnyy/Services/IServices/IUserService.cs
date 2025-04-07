using FKA.Krivosinnyy.BLL.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IUserService
    {
        Task<SignInResult> Login(LoginViewModel model);
        public List<UserViewModel> AllUsers();
        public UserViewModel GetUser(UInt32 userId);
        public UserViewModel UpdateUser(UInt32 userId);
        public void UpdateUser(UserViewModel user);
        public void DeleteUser(UInt32 userId);
    }
}
