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
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IMapper _mapper;
        public RelationshipService(IPersonRepository personRepository, IMapper mapper, 
                IRelationshipRepository relationshipRepository) 
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _relationshipRepository = relationshipRepository;
        }
        public PersonRelationsViewModel EditPersonRelations(int personId)
        {
            var model = new PersonRelationsViewModel();
            var person = _personRepository.Get(personId);
            var allPersons = _personRepository.GetAll();

            var allPersonsWithRelType = new List<PersonWithRelTypeExt>();
            foreach (var p in allPersons)
            {
                allPersonsWithRelType.Add(new PersonWithRelTypeExt() 
                {  
                    Avatar = p.Avatar,
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    RelationType = Relation.Girlfriend 
                });
            }
            model.Person = person;
            model.PersonId = personId;
            model.RelatedPersons = allPersonsWithRelType;

            return model;
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

        public void RemovePersonRelation(int personId, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
