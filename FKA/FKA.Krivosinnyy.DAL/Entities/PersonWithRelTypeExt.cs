using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    public class PersonWithRelTypeExt : Person
    {
        private Person pers;

        public PersonWithRelTypeExt(Person pers)
        {
            this.pers = pers;
        }

        public Relation RelationType { get; set; }
    }
}
