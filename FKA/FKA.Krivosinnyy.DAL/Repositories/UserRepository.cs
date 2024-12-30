using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected DbContext _db;
        public DbSet<User> Users { get; private set; }
        public UserRepository(MyFamilyContext db) 
        { 
            _db = db;
            var set = _db.Set<User>();
            set.Load();

            Users = set;
        }
        public void Create(User item)
        {
            Users.Add(item);
            _db.SaveChanges();
        }

        public void Delete(User item)
        {
            Users.Remove(item);
            _db.SaveChanges();
        }

        public User Get(UInt32 Id)
        {
            return Users.Find(Id);
        }

        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public void Update(User item)
        {
            Users.Update(item);
            _db.SaveChanges();
        }
    }
}
