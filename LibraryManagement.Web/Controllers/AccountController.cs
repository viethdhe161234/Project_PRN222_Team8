using LibraryManagement.Service.Inplements;
using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;

        public AccountController(IAccountService accountService, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var account = _accountService.Login(model.Username, model.Password);

            if (account == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            HttpContext.Session.SetString("Username", account.Username);
            HttpContext.Session.SetString("Role", account.Role.Name);
            HttpContext.Session.SetString("MemberId", account.MemberId.ToString());

            if (account.Role.Name == "Manager")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            if (account.Role.Name == "Librarian")
            {
                return RedirectToAction("Dashboard", "Librarian");
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _accountService.Register(model.Username, model.Email, model.Password);
                TempData["RegisteredUsername"] = model.Username;
                return RedirectToAction("MemberInfo");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public IActionResult MemberInfo()
        {
            if (TempData["RegisteredUsername"] == null)
                return RedirectToAction("Register");

            TempData.Keep("RegisteredUsername");
            return View();
        }
        [HttpPost]
        public IActionResult MemberInfo(MemberInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData.Keep("RegisteredUsername");
                return View(model);
            }

            var username = TempData["RegisteredUsername"]?.ToString();
            if (username == null)
                return RedirectToAction("Register");

            try
            {
                _accountService.CompleteMemberInfo(username, model.FullName, model.Phone, model.Address);
                TempData["Success"] = "Registration successful. Please login.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData.Keep("RegisteredUsername");
                return View(model);
            }

        }
        [AuthorizeRole("Manager")]
        public IActionResult Index(string? searchTerm)
        {
            var accounts = _accountService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                accounts = accounts.Where(a =>
                    a.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    a.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (a.Member != null && a.Member.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                );
            }

            var model = new AccountIndexViewModel
            {
                SearchTerm = searchTerm,
                Accounts = accounts
            };
            return View(model);
        }

        [AuthorizeRole("Manager")]
        public IActionResult Create()
        {
            var model = new CreateAccountViewModel
            {
                Roles = GetStaffRoles()
            };
            return View(model);
        }

        [AuthorizeRole("Manager")]
        [HttpPost]
        public IActionResult Create(CreateAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = GetStaffRoles();
                return View(model);
            }

            try
            {
                _accountService.CreateAccount(
                    model.Username, model.Email, model.Password,
                    model.RoleName, model.FullName, model.Phone, model.Address
                );
                TempData["Success"] = "Account created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.Roles = GetStaffRoles();
                return View(model);
            }
        }

        [AuthorizeRole("Manager")]
        public IActionResult Edit(Guid id)
        {
            var account = _accountService.GetById(id);
            if (account == null) return NotFound();

            var model = new EditAccountViewModel
            {
                Id = account.Id,
                Username = account.Username,
                Email = account.Email,
                RoleName = account.Role.Name,
                Roles = GetStaffRoles()
            };
            return View(model);
        }

        [AuthorizeRole("Manager")]
        [HttpPost]
        public IActionResult Edit(EditAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = GetStaffRoles();
                return View(model);
            }

            try
            {
                _accountService.UpdateAccount(
                    model.Id, model.Username, model.Email, model.RoleName
                );
                TempData["Success"] = "Account updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.Roles = GetStaffRoles();
                return View(model);
            }
        }

        [AuthorizeRole("Manager")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var currentUsername = HttpContext.Session.GetString("Username")!;
            try
            {
                _accountService.DeleteAccount(id, currentUsername);
                TempData["Success"] = "Account deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [AuthorizeRole("Manager")]
        [HttpPost]
        public IActionResult SetActive(Guid id, bool isActive)
        {
            var currentUsername = HttpContext.Session.GetString("Username")!;
            try
            {
                _accountService.SetActiveStatus(id, isActive, currentUsername);
                TempData["Success"] = isActive ? "Account activated" : "Account deactivated";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetStaffRoles()
        {
            return _roleService.GetAllRoles()
                .Where(r => r.Name != "User")
                .Select(r => new SelectListItem(r.Name, r.Name))
                .ToList();
        }
        public IActionResult ForgotPassword()
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var resetBaseUrl = $"{Request.Scheme}://{Request.Host}/Account/ResetPassword";
                _accountService.ForgotPassword(email, resetBaseUrl);
                TempData["Success"] = "Password reset link sent! Please check your email.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }

        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login");

            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                _accountService.ResetPassword(model.Token, model.NewPassword);
                TempData["Success"] = "Password reset successfully. Please login.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
