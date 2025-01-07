using FKA.Krivosinnyy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = FKA.Krivosinnyy.DAL.Entities.File;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public interface IFileRepository
    {
        IEnumerable<File> GetAll();
        File Get(int Id);
        void Add(File item);
        void Delete(File item);
    }
}
