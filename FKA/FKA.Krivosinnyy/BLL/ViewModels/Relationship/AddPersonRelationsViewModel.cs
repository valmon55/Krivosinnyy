
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.DAL.Entities;
using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class AddPersonRelationsViewModel
    {
        public int Id { get; set; }
        public PersonViewModel? Person { get; set; }
        public Dictionary<PersonWithRelTypeExt, bool> CheckedPersons { get; set; }
        public List<PersonExtRelTypeViewModel>? RelatedPersons { get; set; }
    }
}
