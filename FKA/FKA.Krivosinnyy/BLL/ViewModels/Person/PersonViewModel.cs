using FKA.Krivosinnyy.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace FKA.Krivosinnyy.BLL.ViewModels.Person
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        [Display(Name = "Год")]
        public int? Year { get; set; }
        [Display(Name = "Месяц")]
        public int? Month { get; set; }
        [Display(Name = "День")]
        public int? Day { get; set; }

    }
}
