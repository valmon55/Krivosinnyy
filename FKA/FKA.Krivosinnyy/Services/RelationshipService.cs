using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.Relationship;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
        public AddPersonRelationsViewModel EditPersonRelations(int personId)
        {
            var model = new AddPersonRelationsViewModel();
            var person = _personRepository.Get(personId);
            //все кроме самого себя
            var allPersons = _personRepository.GetAll().Where(x => x.Id != personId);

            var allPersonsWithRelType = new List<PersonExtRelTypeViewModel>();
            foreach (var p in allPersons)
            {
                allPersonsWithRelType.Add(_mapper.Map<PersonExtRelTypeViewModel>(p));
            }
            model.Id = personId;
            model.Person = _mapper.Map<PersonViewModel>(person);            
            model.RelatedPersons = allPersonsWithRelType;

            /// В списке всех возможных персон отмечаем взаимосвязи
            var checkedPersons = new Dictionary<PersonWithRelTypeExt, bool>();
            var relationPersons = _relationshipRepository.GetAllByPerson(personId);

            foreach(var pers in allPersons)
            {
                var p = new PersonWithRelTypeExt(pers);
                checkedPersons.Add(p, false);
                foreach(var relationPerson in relationPersons)
                {
                    if(pers == relationPerson)
                    {
                        checkedPersons[p] = true;
                    }
                }
            }

            return model;
        }
        /// <summary>
        /// Для 1 персоны Имеем список выбранных персон с типом связи
        /// </summary>
        /// <param name="model"></param>
        public void SavePersonRelations(AddPersonRelationsViewModel model)
        //public void SavePersonRelations(int )
        {
            var relations = new List<Relationship>();
            var rel = new Relationship();
            rel.Person = _mapper.Map<Person>(model.Person);
            // перебираем связи по одной
            foreach (var relatedPersonViewModel in model.RelatedPersons)
            {
                rel.RelatedPerson = _mapper.Map<PersonWithRelTypeExt>(relatedPersonViewModel);
                rel.Relation = (Relation)relatedPersonViewModel.Relation;
                _relationshipRepository.Add(rel);
            }
        }
        public void SavePersonRelations(int personId, List<int> relatedPersons)
        {
            var pers = new Person();
            foreach (var relatedPerson in relatedPersons)
            {
                pers = _personRepository.Get(relatedPerson);
                if (pers != null)
                {
                    _relationshipRepository.Add(personId, pers);
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

        public void RemovePersonRelation(int personId, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
