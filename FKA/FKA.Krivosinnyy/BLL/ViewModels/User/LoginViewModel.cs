using System.ComponentModel.DataAnnotations;

namespace FKA.Krivosinnyy.BLL.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле Email обязательно к заполнению")]
        [EmailAddress(ErrorMessage = "Поле Email обязательно к заполнению")]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(24, ErrorMessage = "Пароль должен иметь не менее {0} символов", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
