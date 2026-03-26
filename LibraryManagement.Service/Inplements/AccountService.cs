using LibraryManagement.Domain.Entities;
using LibraryManagement.Repository.Implements;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Inplements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        public AccountService(IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        public void ChangePassword(string username, string currentPassword, string newPassword)
        {
            var account = _accountRepository.GetByUsername(username);
            if (account == null)
                throw new Exception("Account not found");

            if (!BCrypt.Net.BCrypt.Verify(currentPassword, account.PasswordHash))
                throw new Exception("Current password is incorrect");

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _accountRepository.Update(account);
        }

        public void CompleteMemberInfo(string username, string fullName, string? phone, string? address)
        {
            var account = _accountRepository.GetByUsername(username);
            if (account == null)
                throw new Exception("Account not found");

            account.Member.FullName = fullName;
            account.Member.Phone = phone;
            account.Member.Address = address;

            _accountRepository.Update(account);
        }

        public void CreateAccount(string username, string email, string password, string roleName, string fullName, string? phone, string? address)
        {
            if (roleName == "User")
                throw new Exception("Cannot create User account from admin panel");
            if (_accountRepository.GetByUsername(username) != null)
                throw new Exception("Username already exists");

            if (_accountRepository.GetByEmail(email) != null)
                throw new Exception("Email already exists");

            var role = _roleRepository.GetByName(roleName);
            if (role == null)
                throw new Exception($"Role '{roleName}' not found");

            var memberId = Guid.NewGuid();

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsActive = true,
                IsEmailVerified = true,
                RoleId = role.Id,
                MemberId = memberId,
                Member = new Member
                {
                    Id = memberId,
                    FullName = fullName,
                    Phone = phone,
                    Address = address
                }
            };

            _accountRepository.Add(account);
        }

        public void DeleteAccount(Guid id, string currentUsername)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
                throw new Exception("Account not found");

            if (account.Role.Name == "User")
                throw new Exception("Cannot delete User account");

            if(account.Role.Name == "Manager")
        throw new Exception("Cannot delete Manager account");
            if (account.Username == currentUsername)
                throw new Exception("Cannot delete your own account");

            _accountRepository.Delete(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountRepository.GetAll();
        }

        public Account? GetById(Guid id)
        {
            return _accountRepository.GetById(id);
        }

        public Account? Login(string username, string password)
        {
            var account = _accountRepository.GetByUsername(username);
            if (account == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                return null;
            if (!account.IsActive)
                return null;
            return account;
        }

        public void Register(string username, string email, string password)
        {
            if (_accountRepository.GetByUsername(username) != null)
                throw new Exception("Username already exists");

            if (_accountRepository.GetByEmail(email) != null)
                throw new Exception("Email already exists");

            var userRole = _roleRepository.GetByName("User");
            if (userRole == null)
                throw new Exception("Default role not found");
            var memberId = Guid.NewGuid();
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                IsActive = true,
                IsEmailVerified = false,
                RoleId = userRole.Id,
                MemberId = memberId,
                Member = new Member
                {
                    Id = memberId,
                    FullName = username
                }
            };

            _accountRepository.Add(account);
        }

        public void SetActiveStatus(Guid id, bool isActive, string currentUsername)
        {
            var account = _accountRepository.GetById(id);

            if (account == null)
                throw new Exception("Account not found");

            if (account.Username == currentUsername)
                throw new Exception("Cannot change status of your own account");

            account.IsActive = isActive;
            _accountRepository.Update(account);
        }

        public void UpdateAccount(Guid id, string username, string email, string roleName)
        {
            var account = _accountRepository.GetById(id);
            if (account == null)
                throw new Exception("Account not found");
            if (account.Role.Name == "User")
                throw new Exception("Cannot update User account");

            // Check trùng username nếu đổi
            var existingByUsername = _accountRepository.GetByUsername(username);
            if (existingByUsername != null && existingByUsername.Id != id)
                throw new Exception("Username already exists");

            // Check trùng email nếu đổi
            var existingByEmail = _accountRepository.GetByEmail(email);
            if (existingByEmail != null && existingByEmail.Id != id)
                throw new Exception("Email already exists");

            var role = _roleRepository.GetByName(roleName);
            if (role == null)
                throw new Exception($"Role '{roleName}' not found");

            account.Username = username;
            account.Email = email;
            account.RoleId = role.Id;

            _accountRepository.Update(account);
        }
    }
}
