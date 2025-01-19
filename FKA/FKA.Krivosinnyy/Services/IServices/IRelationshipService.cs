using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.Relationship;
using FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IRelationshipService
    {
        public List<PersonRelationsViewModel> AllPersonRelations(int personId);
        public PersonRelationsViewModel EditPersonRelations(int personId);
        public void AddPersonRelation(int personId, Person person);
        public void RemovePersonRelation(int personId, Person person);
    }
}
