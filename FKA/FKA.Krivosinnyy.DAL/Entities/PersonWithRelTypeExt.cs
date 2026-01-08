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
        public PersonWithRelTypeExt SetRelType(Person person, Relation relation, bool is_rev = false )
        {
            Relation rel = relation;
            if (is_rev)
            {
                switch(relation) 
                {
                    case Relation.Wife: rel = Relation.Husband;  break;
                    case Relation.Husband: rel = Relation.Wife;  break;
                    case Relation.Groom: rel = Relation.Bride;  break;
                    case Relation.Bride: rel = Relation.Groom; break;
                    case Relation.Father: rel = person.gender == Gender.Man ? Relation.Son : Relation.Daughter; break;
                    case Relation.Mother: rel = person.gender == Gender.Man ? Relation.Son : Relation.Daughter; break;
                    default: rel = relation; break;
                };
            }

            return new PersonWithRelTypeExt()
            {
                Id = person.Id,
                Avatar = person.Avatar,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                gender = person.gender,
                RelationType = rel,
            };
        }
    }
}
