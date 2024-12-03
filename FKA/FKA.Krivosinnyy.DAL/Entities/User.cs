using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    public class User : IdentityUser
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Middle_Name { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
