using FKA.Krivosinnyy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public interface IRelationshipRepository
    {
        public IEnumerable<Relationship> GetAll();
        IEnumerable<PersonWithRelTypeExt> GetAllWithRelType();
        IEnumerable<Person> GetAllByPerson(int Id);
        IEnumerable<PersonWithRelTypeExt> GetAllByPersonWithRelType(int Id);
        void Add(int Id, Person item);
        void Remove(int Id, Person item);
        public void Update(Relationship item);
    }
}
