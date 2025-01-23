using FKA.Krivosinnyy.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace FKA.Krivosinnyy.BLL.ViewModels.Person
{
    public class PersonExtRelTypeViewModel : PersonViewModel
    {
        public Relation? Relation { get; set; }

    }
}
