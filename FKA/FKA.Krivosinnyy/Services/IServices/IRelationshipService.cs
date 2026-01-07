using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.Relationship;
using FKA.Krivosinnyy.DAL.Entities;

namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IRelationshipService
    {
        public List<PersonRelationsViewModel> AllPersonRelations(int personId);
        public RelationshipViewModel GetRelation(int personId, int relatedPersonId);
        public AllRelationshipsViewModel AllRelationships();
        public void SetRelation(RelationshipViewModel model);
        public EditPersonRelationsViewModel PersonRelations(int personId);
        public EditPersonRelationsViewModel EditPersonRelations(int personId);
        public void EditPersonRelations(EditPersonRelationsViewModel model, List<int> SelectedPersons);
        public void AddPersonRelation(int personId, Person person);
        public void RemovePersonRelation(int personId, Person person);
    }
}
