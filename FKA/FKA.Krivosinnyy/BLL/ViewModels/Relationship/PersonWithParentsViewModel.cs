using Entity = FKA.Krivosinnyy.DAL.Entities;
namespace FKA.Krivosinnyy.BLL.ViewModels.Relationship
{
    public class PersonWithParentsViewModel
    {
        public int? Level { get; set; }
        public Entity.Person Person { get; set; }
        public Entity.Person? Person_Father { get; set; }
        public Entity.Person? Person_Mother { get; set; }
    }
}
