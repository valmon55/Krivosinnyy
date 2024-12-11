using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Entities
{
    public class Role : IdentityRole<UInt32>
    {
        public override UInt32 Id { get; set; }
        public string Description { get; set; }
    }
}
