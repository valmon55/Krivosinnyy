using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    public class PersonWithRelTypeExt : Person
    {
        public Relation RelationType { get; set; }
    }
}
