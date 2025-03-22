using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.Extentions
{
    public static class UserFromModel
    {
        public static User Convert(this User user, UserViewModel userViewModel)
        {
            user.Id = userViewModel.Id;
            user.First_Name = userViewModel.First_Name;
            user.Middle_Name = userViewModel.Middle_Name;
            user.Last_Name = userViewModel.Last_Name;
            user.BirthDate = new DateTime((int)userViewModel.Year, (int)userViewModel.Month, (int)userViewModel.Day);
            user.Email = userViewModel.Email;
            user.UserName = userViewModel.Login;
            user.Foto = userViewModel.Foto;

            return user;
        }
    }
}
