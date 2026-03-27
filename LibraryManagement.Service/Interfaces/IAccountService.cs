using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Service.Interfaces
{
    public interface IAccountService
    {
        Account? Login(string username, string password);
        // Member tự đăng ký
        void Register(string username, string email, string password);
        void CompleteMemberInfo(string username, string fullName, string? phone, string? address);

        // Admin tạo tài khoản
        void CreateAccount(string username, string email, string password, string roleName, string fullName, string? phone, string? address);
        IEnumerable<Account> GetAll();
        Account? GetById(Guid id);
        void UpdateAccount(Guid id, string username, string email, string roleName);
        void SetActiveStatus(Guid id, bool isActive, string currentUsername);
        void DeleteAccount(Guid id, string currentUsername);
        void ChangePassword(string username, string currentPassword, string newPassword);
        void UpdateProfile(string username, string newUsername, string newEmail);
        void ForgotPassword(string email, string resetBaseUrl);
        void ResetPassword(string token, string newPassword);
    }
}
