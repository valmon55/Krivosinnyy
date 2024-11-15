﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace FKA.Krivosinnyy.BLL.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя", Prompt = "Введите имя")]
        public string First_Name {  get; set; }        
        
        public string Middle_Name { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string Last_Name { get; set; }
        
        [Required(ErrorMessage = "Поле Email обязательно к заполнению")]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Год")]
        public int? Year { get; set; }

        [Required]
        [Display(Name = "Месяц")]
        public int? Month { get; set; }

        [Required]
        [Display(Name = "День")]
        public int? Day { get; set; }

        [Required(ErrorMessage = "Обязательно к заполнению")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(24, ErrorMessage = "Пароль должен иметь не менее {0} символов", MinimumLength = 6)]
        public string PasswordReg { get; set; }

        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль еще раз")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Поле Логин обязательно к заполнению")]
        [DataType(DataType.Text)]
        [Display(Name = "Логин", Description = "Введите логин")]
        public string Login {  get; set; }
    }
}
