using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
    }
}
