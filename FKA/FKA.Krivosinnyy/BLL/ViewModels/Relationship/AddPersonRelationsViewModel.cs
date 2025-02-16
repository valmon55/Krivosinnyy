
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class AddPersonRelationsViewModel
    {
        public int Id { get; set; }
        public PersonViewModel? Person { get; set; }
        public List<PersonExtRelTypeViewModel>? RelatedPersons { get; set; }
    }
}
