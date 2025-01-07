using FKA.Krivosinnyy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = FKA.Krivosinnyy.DAL.Entities.File;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        protected DbContext _db;
        public DbSet<File> Files { get; private set; }
        public FileRepository(MyFamilyContext db) 
        { 
            _db = db;
            var set = _db.Set<File>();
            set.Load();

            Files = set;
        }
        public void Add(File item)
        {
            Files.Add(item);
            _db.SaveChanges();
        }

        public void Delete(File item)
        {
            Files.Remove(item);
            _db.SaveChanges();
        }

        public File Get(int Id)
        {
            return Files.Find(Id);
        }

        public IEnumerable<File> GetAll()
        {
            return Files;
        }
    }
}
