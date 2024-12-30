using FKA.Krivosinnyy.BLL.ViewModels.User;

namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IUserService
    {
        public List<UserViewModel> AllUsers();
        public UserViewModel UpdateUser(UInt32 userId);
        public void UpdateUser(UserViewModel user);
        public void DeleteUser(UInt32 userId);
    }
}
