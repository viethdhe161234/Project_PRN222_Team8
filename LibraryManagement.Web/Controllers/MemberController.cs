using LibraryManagement.Service.Interfaces;
using LibraryManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAll();
            return View(members);
        }
        public IActionResult Edit(Guid id)
        {
            var member = _memberService.GetById(id);
            if (member == null) return NotFound();

            var model = new EditMemberViewModel
            {
                Id = member.Id,
                FullName = member.FullName,
                Phone = member.Phone,
                Address = member.Address
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditMemberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _memberService.Update(model.Id, model.FullName, model.Phone, model.Address);
                TempData["Success"] = "Member updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
    }
}
