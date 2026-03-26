using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role? GetById(Guid id);
        Role? GetByName(string name);
        void Add(Role role);
        void Update(Role role);
        void Delete(Guid id);
    }
}
