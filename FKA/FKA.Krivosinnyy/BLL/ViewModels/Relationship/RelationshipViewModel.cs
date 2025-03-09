using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class RelationshipViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Entity.Person Person { get; set; }
        public int RelatedPersonId { get; set; }
        public Entity.Person RelatedPerson { get; set; }
        public Entity.Relation Relation { get; set; }
    }
}
