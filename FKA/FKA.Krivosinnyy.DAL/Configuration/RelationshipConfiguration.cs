using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Configuration
{
    public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.ToTable("Relationship").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
