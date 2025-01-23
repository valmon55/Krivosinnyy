
using Entity = FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    /// <summary>
    /// 1 взаимосвязь
    /// </summary>
    public class PersonRelationViewModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Entity.Person Person { get; set; }
        public Entity.PersonWithRelTypeExt RelatedPerson { get; set; }
    }
}
