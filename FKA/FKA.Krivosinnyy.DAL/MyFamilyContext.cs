using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FKA.Krivosinnyy.DAL
{
    public class MyFamilyContext :IdentityDbContext<User> 
    {
        override public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public MyFamilyContext(DbContextOptions<MyFamilyContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
