using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace FKA.Krivosinnyy.BLL.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [Required]
        public UInt32 Id { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Text)]
        [Display(Name = "Текущий пароль", Description = "Введите текущий пароль")]
        [StringLength(24, ErrorMessage = "Пароль должен иметь не менее {0} символов", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите новый пароль")]
        [StringLength(24, ErrorMessage = "Пароль должен иметь не менее {0} символов", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль еще раз")]
        public string NewPasswordConfirm { get; set; }
    }
}
