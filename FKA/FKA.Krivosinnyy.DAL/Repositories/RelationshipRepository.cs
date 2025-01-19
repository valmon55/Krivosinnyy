using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        protected DbContext _db;
        public DbSet<Person> Persons { get; private set; }
        public DbSet<Relationship> Relationships { get; private set; }
        public RelationshipRepository(MyFamilyContext db) 
        { 
            _db = db;
            var personSet = _db.Set<Person>();
            personSet.Load();
            
            Persons = personSet;

            var relationshipSet = _db.Set<Relationship>();
            relationshipSet.Load();

            Relationships = relationshipSet;

        }

        public IEnumerable<Person> GetAllByPerson(int Id)
        {
            var rels = Relationships.Include(c => c.RelatedPersonId).Where(x => x.Id == Id);   
            var pers = new List<Person>();
            rels.ForEachAsync(x => pers.Add(x.RelatedPerson));
            
            return pers;
        }

        public void Add(int Id, Person item)
        {
            // Себя самого не добавляем
            if (Id == item.Id)
                return;
            //не дублируем
            var rels = Relationships.Include(c => c.RelatedPersonId).Where(x => x.Id == Id);
            foreach (var r in rels)
            {
                if (r.RelatedPerson == item) 
                { 
                    return; 
                }
            }

            var p = Persons.Where(x => x.Id == Id).FirstOrDefault();
            var newRel = new Relationship() 
            { 
                PersonId = Id, 
                Person = p, 
                RelatedPersonId = item.Id, 
                RelatedPerson = item, 
                Relation = Relation.Girlfriend // 
            };
            Relationships.Add(newRel);

            //нужно ли добавлять отношения с противоположной стороны ??

            _db.SaveChanges();
        }

        public void Remove(int Id, Person item)
        {
            var rels = Relationships.Include(c => c.RelatedPersonId).Where(x => x.Id == Id);
            foreach (var r in rels)
            {
                if (r.RelatedPerson == item)
                {
                    Relationships.Remove(r);
                    _db.SaveChanges();

                    return;
                }
            }
        }
    }
}
