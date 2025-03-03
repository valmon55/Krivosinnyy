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

            var allPersonsWithRelType = _relationshipRepository.GetAllWithRelType().ToList();
            foreach(var relation in allPersonsWithRelType)
            {
                relation.Avatar ??= new DAL.Entities.File() { Path = String.Empty };
            }
            var myRelPersonsWithRelType = _relationshipRepository.GetAllByPersonWithRelType(personId);
            foreach (var relation in myRelPersonsWithRelType)
            {
                relation.Avatar ??= new DAL.Entities.File() { Path = String.Empty };
            }

            var relPers = new Dictionary<int, bool>();

            foreach (var p in allPersonsWithRelType)
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
        /// <summary>
        /// Для 1 персоны Имеем список выбранных персон с типом связи
        /// </summary>
        /// <param name="model"></param>
        public void EditPersonRelations(EditPersonRelationsViewModel model, List<int> SelectedPersons)
        {
            // перебираем связи по одной
            foreach(var relatedPerson in model.RelatedPersons)
            {
                foreach(var person in SelectedPersons)
                {
                    //if(relatedPerson.Id )
                }
            }
            //_relationshipRepository.Add();
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
