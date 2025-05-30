using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.Relationship;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;

namespace FKA.Krivosinnyy.Services
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IMapper _mapper;
        public RelationshipService(IPersonRepository personRepository, IMapper mapper,
                IFileRepository fileRepository,
                IRelationshipRepository relationshipRepository) 
        {
            _personRepository = personRepository;
            _fileRepository = fileRepository;
            _mapper = mapper;
            _relationshipRepository = relationshipRepository;
        }
        public EditPersonRelationsViewModel EditPersonRelations(int personId)
        {
            var model = new EditPersonRelationsViewModel();
            var person = _personRepository.Get(personId);

            var allPersons = _personRepository.GetAll().Where(x => x.Id != personId).ToList();
            var allPersonsWithRelType = new List<PersonWithRelTypeExt>();
            foreach (var pers in allPersons)
            {
                pers.Avatar ??= new DAL.Entities.File() { Path = String.Empty };
                allPersonsWithRelType.Add(_mapper.Map<PersonWithRelTypeExt>(pers));
            }
            var myRelPersonsWithRelType = _relationshipRepository.GetAllByPersonWithRelType(personId);
            foreach (var relation in myRelPersonsWithRelType)
            {
                relation.Avatar ??= new DAL.Entities.File() { Path = String.Empty };
            }

            var relPers = new Dictionary<int, bool>();

            foreach (var p in allPersons)
            {
                relPers.Add(p.Id, false);
                foreach(var m in myRelPersonsWithRelType)
                {
                    if (m.Id == p.Id)
                    {
                        relPers[p.Id] = true;
                    }
                }
            }
            model.Person = person;            
            model.RelatedPersons = allPersonsWithRelType;
            model.CheckedRelatedPersonIds = relPers;

            return model;
        }
        public EditPersonRelationsViewModel PersonRelations(int personId)
        {
            var model = new EditPersonRelationsViewModel();
            var person = _personRepository.Get(personId);

            person.Avatar ??= new DAL.Entities.File() { Path = String.Empty };

            var myRelPersonsWithRelType = _relationshipRepository.GetAllByPersonWithRelType(personId).ToList();
            foreach (var relation in myRelPersonsWithRelType)
            {
                relation.Avatar ??= new DAL.Entities.File() { Path = String.Empty };
            }

            model.Person = person;
            model.RelatedPersons = myRelPersonsWithRelType;

            return model;
        }

        /// <summary>
        /// Для 1 персоны Имеем список выбранных персон с типом связи
        /// </summary>
        /// <param name="model"></param>
        public void EditPersonRelations(EditPersonRelationsViewModel model, List<int> SelectedPersonIds)
        {
            var relatedPersons = _relationshipRepository.GetAllByPerson(model.PersonId).ToList();
            var selectedPersons = new List<Person>();

            if (relatedPersons != null)
            {
                foreach (var personId in SelectedPersonIds)
                {
                    selectedPersons.Add(_personRepository.Get(personId));
                }
                var personsToRemove = relatedPersons.Where(r => !selectedPersons.Any(s => s.Id == r.Id)).ToList();
                foreach (var person in personsToRemove)
                {
                    _relationshipRepository.Remove(model.PersonId, person);
                }
                var personsToAdd = selectedPersons.Where(s => !relatedPersons.Any(r => r.Id == s.Id)).ToList();
                foreach(var person in personsToAdd)
                {
                    _relationshipRepository.Add(model.PersonId, person);
                }
            }
        }
        public void AddPersonRelation(int personId, Person person)
        {
            _relationshipRepository.Add(personId, person);
        }

        public List<PersonRelationsViewModel> AllPersonRelations(int personId)
        {
            throw new NotImplementedException();
            //return _relationshipRepository.GetAllByPersonId(personId);
        }
        public AllRelationshipsViewModel AllRelationships()
        {
            var rels = _relationshipRepository.GetAll().ToList();
            var allRelsViewModel = new AllRelationshipsViewModel() { Relationships = new List<RelationshipViewModel>() };
            foreach(var rel in rels)
            {
                allRelsViewModel.Relationships.Add(_mapper.Map<RelationshipViewModel>(rel));
            }
            //relsViewModel.Relationships.Add(_mapper.Map<RelationshipViewModel>(rels));

            return allRelsViewModel;
        }
        public RelationshipViewModel GetRelation(int personId, int relatedPersonId)
        {
            var rel = _relationshipRepository.GetAll().Where(p => p.Person.Id == personId)
                                                      .Where(r => r.RelatedPerson.Id == relatedPersonId).ElementAt(0);
            var relViewModel = new RelationshipViewModel()
            {
                Id = rel.Id,
                PersonId = rel.PersonId,
                Person = rel.Person,
                RelatedPersonId = rel.RelatedPersonId,
                RelatedPerson = rel.RelatedPerson,
                Relation = rel.Relation
            };
            return relViewModel;
        }
        public PersonWithParentsViewModel GetPersonParents(int personId)
        {
            var father = _personRepository.Get(
                    _relationshipRepository.GetAll().
                    FirstOrDefault(p => p.PersonId == personId &&
                        p.Relation == Relation.Father).RelatedPersonId);

            var mother = _personRepository.Get(
                    _relationshipRepository.GetAll().
                    FirstOrDefault(p => p.PersonId == personId &&
                        p.Relation == Relation.Mother).RelatedPersonId);

            return new PersonWithParentsViewModel()
            {
                Level = 0,
                Person = _personRepository.Get(personId),
                Person_Father = father,
                Person_Mother = mother
            };
        }
        public List<PersonWithParentsViewModel> GetAllPersonWithParents()
        {
            var personsWithParents = new List<PersonWithParentsViewModel>();
            var personWithParents = new PersonWithParentsViewModel();
            foreach (var p in _personRepository.GetAll())
            {
                personWithParents = GetPersonParents(p.Id);
                if(personWithParents != null)
                {
                    personsWithParents.Add(personWithParents);
                }
            }
            /// надо заполнить level
            int level = 0;
            var personsWithOutParents = personsWithParents.Where(p => p.Person_Father is null && p.Person_Mother is null);
            foreach(var p in personsWithOutParents)
            {

            }

            return personsWithParents;
        }
        public void SetRelation(RelationshipViewModel model)
        {
            var relationship = _relationshipRepository.GetAll().Where(p => p.Person.Id == model.PersonId)
                                                      .Where(r => r.RelatedPerson.Id == model.RelatedPersonId).SingleOrDefault();
            if (relationship != null)
            {
                relationship.Relation = model.Relation;
                _relationshipRepository.Update(relationship);
            }
        }
    }
}
