using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Role? GetRoleById(Guid id);
        void CreateRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(Guid id);
    }
}
