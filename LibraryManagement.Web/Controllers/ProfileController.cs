using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Filters;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    [AuthorizeRole("User", "Manager", "Librarian")]
    public class ProfileController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IAccountService _accountService;

        public ProfileController(IMemberService memberService, IAccountService accountService)
        {
            _memberService = memberService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var memberId = Guid.Parse(HttpContext.Session.GetString("MemberId")!);
            var member = _memberService.GetById(memberId);
            if (member == null) return NotFound();

            var model = new ProfileViewModel
            {
                FullName = member.FullName,
                Phone = member.Phone,
                Address = member.Address,
                Username = member.Account?.Username,
                Email = member.Account?.Email
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateInfo(ProfileViewModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            var memberId = Guid.Parse(HttpContext.Session.GetString("MemberId")!);
            try
            {
                _memberService.Update(memberId, model.FullName, model.Phone, model.Address);
                TempData["Success"] = "Profile updated successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);

            var username = HttpContext.Session.GetString("Username")!;
            try
            {
                _accountService.ChangePassword(username, model.CurrentPassword, model.NewPassword);
                TempData["Success"] = "Password changed successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}