using System.ComponentModel.DataAnnotations;

namespace FKA.Krivosinnyy.BLL.ViewModels.User
{
    public class ConfirmAccountViewModel
    {
        [Required]
        public UInt32 Id { get; set; }

        [Required(ErrorMessage = "Поле Email обязательно к заполнению")]
        [EmailAddress(ErrorMessage = "Поле Email обязательно к заполнению")]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Text)]
        [Display(Name = "Код подтверждния", Description = "Введите код подтверждения из вашего почтового ящика")]
        public string Code { get; set; }
    }
}
