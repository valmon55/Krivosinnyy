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
        public PersonWithRelTypeExt SetRelType(Person person, Relation relation)
        {
            return new PersonWithRelTypeExt()
            {
                Id = person.Id,
                Avatar = person.Avatar,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                gender = person.gender,
                RelationType = relation,
            };
        }
    }
}
