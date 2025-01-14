
using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class PersonRelationsViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Entity.Person Person { get; set; }
        public List<Entity.Person> RelatedPersons { get; set; }
    }
}
