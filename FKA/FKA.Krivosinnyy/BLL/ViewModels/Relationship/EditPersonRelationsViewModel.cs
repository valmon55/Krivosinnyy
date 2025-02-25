using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class EditPersonRelationsViewModel
    {
        public Entity.Person Person { get; set; }
        public List<Entity.PersonWithRelTypeExt> RelatedPersons { get; set; }
        public Dictionary<int, bool> CheckedRelatedPersonIds { get; set; }
    }
}
