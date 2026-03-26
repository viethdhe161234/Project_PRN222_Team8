using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public void CreateRole(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                throw new Exception("Role name is required");
            }

            var existingRole = _roleRepository.GetByName(role.Name);

            if (existingRole != null)
            {
                throw new Exception("Role already exists");
            }

            _roleRepository.Add(role);
        }

        public void DeleteRole(Guid id)
        {
            _roleRepository.Delete(id);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }

        public Role? GetRoleById(Guid id)
        {
            return _roleRepository.GetById(id);
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.Update(role);
        }
    }
}
