using FKA.Krivosinnyy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKA.Krivosinnyy.DAL.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(UInt32 Id);
        void Create(User item);
        void Update(User item);
        void Delete(User item);
    }
}
