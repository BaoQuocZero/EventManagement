using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace demo_02.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly EventManagementContext _context;
        private readonly EventService _eventService;

        public ProfileModel(EventManagementContext context, EventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        [BindProperty]
        public User User { get; set; }  // Người dùng cần chỉnh sửa

        public List<SelectListItem> RolesList { get; set; } // Danh sách quyền
        public string SessionUserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SessionUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(SessionUserId))
            {
                return RedirectToPage("/Account/Login");
            }

            if (!int.TryParse(SessionUserId, out int userId))
            {
                return RedirectToPage("/Account/Login");
            }

            User = await _eventService.GetUserByIdAsync(userId);

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
                User.UserId, User.RolesId, User.Classid, User.Classname, User.FullName);

            if (!isUpdated)
            {
                ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
                return Page();
            }

            return RedirectToPage(); // Load lại trang Profile
        }
    }
}