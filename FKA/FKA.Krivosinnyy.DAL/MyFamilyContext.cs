using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FKA.Krivosinnyy.DAL
{
    public class MyFamilyContext :IdentityDbContext<User> 
    {
        public override DbSet<User> Users { get; set; }
        //public override DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<RelMain> RelMains { get; set; }
        /// <summary>
        /// О том, почему так сделал см. по ссылке
        /// https://translated.turbopages.org/proxy_u/en-ru.ru.ed1b19d2-67436d7e-1ef0ebad-74722d776562/https/stackoverflow.com/questions/851625/foreign-key-constraint-may-cause-cycles-or-multiple-cascade-paths
        /// </summary>
        /// <param name="options"></param>
        public MyFamilyContext(DbContextOptions<MyFamilyContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Relationship>().ToTable("CK_Different_Persons", "[FirstId] != [SecondId]");
        }
    }
}
