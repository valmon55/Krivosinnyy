using FKA.Krivosinnyy.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace FKA.Krivosinnyy.BLL.ViewModels.Person
{
    public class PersonViewModel
    {
        [Required]
        public string First_Name { get; set; } = string.Empty;
        [Required]
        public string Last_Name { get; set; } = string.Empty;
        public string Middle_Name { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public Gender gender;
        [Display(Name = "Год")]
        public int? Year { get; set; }
        [Display(Name = "Месяц")]
        public int? Month { get; set; }
        [Display(Name = "День")]
        public int? Day { get; set; }

    }
}
