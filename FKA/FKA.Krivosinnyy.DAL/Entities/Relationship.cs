using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    public class Relationship
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        //public int RelationshipId { get; set; }
        //public RelMain RelMains { get; set; }
        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }
        public Relation Relation { get; set; }
    }
}
