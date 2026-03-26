using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LibraryDbContext _context;
        public RoleRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void Add(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role? GetById(Guid id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public Role? GetByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == name);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
