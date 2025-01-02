using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        protected DbContext _db;
        public DbSet<Person> Persons { get; private set; }
        public PersonRepository(MyFamilyContext db) 
        { 
            _db = db;
            var set = _db.Set<Person>();
            set.Load();

            Persons = set;
        }
        public void Create(Person item)
        {
            Persons.Add(item);
            _db.SaveChanges();
        }

        public void Delete(Person item)
        {
            Persons.Remove(item);
            _db.SaveChanges();
        }

        public Person Get(int Id)
        {
            return Persons.Find(Id);
        }

        public IEnumerable<Person> GetAll()
        {
            return Persons;
        }

        public void Update(Person item)
        {
            Persons.Update(item);
            _db.SaveChanges();
        }
    }
}
