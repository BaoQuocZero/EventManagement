using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace demo_02.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly EventService _eventService;

        public EditModel(EventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public User User { get; set; }  // Người dùng cần chỉnh sửa

        public List<SelectListItem> RolesList { get; set; } // Danh sách quyền

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Tìm người dùng theo ID
            User = await _eventService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            // Lấy danh sách quyền
            var roles = await _eventService.GetRolesAsync();
            RolesList = roles.Select(r => new SelectListItem
            {
                Value = r.RolesId.ToString(),
                Text = r.Name
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isUpdated = await _eventService.UpdateUserRoleAndClassAsync(
                User.UserId, User.RolesId, User.Classid, User.Classname);

            if (!isUpdated)
            {
                ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
                return Page();
            }

            return RedirectToPage("./Details", new { id = User.UserId });
        }
    }
}