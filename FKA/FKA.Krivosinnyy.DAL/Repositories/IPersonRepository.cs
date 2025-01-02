using FKA.Krivosinnyy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person Get(int Id);
        void Create(Person item);
        void Update(Person item);
        void Delete(Person item);
    }
}
