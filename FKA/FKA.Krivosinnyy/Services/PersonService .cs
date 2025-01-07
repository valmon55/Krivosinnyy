using AutoMapper;
using FKA.Krivosinnyy.BLL.ViewModels.Person;
using FKA.Krivosinnyy.BLL.ViewModels.User;
using FKA.Krivosinnyy.DAL.Entities;
using FKA.Krivosinnyy.DAL.Repositories;
using FKA.Krivosinnyy.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;

namespace FKA.Krivosinnyy.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository personRepository, IMapper mapper, IFileRepository fileRepository) 
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }
        public void AddPerson(PersonViewModel personView)
        {
            var person = _mapper.Map<Person>(personView);
            _personRepository.Create(person);
        }
        public List<PersonViewModel> AllPersons()
        {
            var persons = _personRepository.GetAll();
            var personsView = new List<PersonViewModel>();

            foreach(var person in persons)
            {
                var personView = _mapper.Map<PersonViewModel>(person);
                personsView.Add(personView);
            }
            return personsView;
        }
        public PersonViewModel ViewPerson(int personId)
        {
            var person = _personRepository.Get(personId);
            return _mapper.Map<PersonViewModel>(person);
        }
        public PersonViewModel UpdatePerson(int personId)
        {
            throw new NotImplementedException();
        }
        public void UpdatePerson(PersonViewModel personView)
        {
            throw new NotImplementedException();
        }
        public void DeletePerson(int personId)
        {
            var person = _personRepository.Get(personId);
            _personRepository.Delete(person);
        }
        public void SetAvatar(int personId, string filePath)
        {
            _fileRepository.Add(
                new DAL.Entities.File()
                {
                    Name = "",
                    Path = filePath,
                    Type = "Image"
                });
        }
    }
}
