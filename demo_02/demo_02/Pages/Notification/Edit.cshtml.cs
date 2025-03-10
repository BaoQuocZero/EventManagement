using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_02.Pages.Notification
{
    public class EditModel : PageModel
    {
        private readonly NotificationService _notificationService;
        private readonly EventManagementContext _context;

        public EditModel(NotificationService notificationService, EventManagementContext context)
        {
            _notificationService = notificationService;
            _context = context;
        }

        [BindProperty]
        public demo_02.Models.Notification Notification { get; set; } = new demo_02.Models.Notification();

        [BindProperty]
        public List<Notificationtype> NotificationTypes { get; set; } = new List<Notificationtype>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Lấy dữ liệu sự kiện theo ID
            Notification = await _context.Notifications.FindAsync(id);
            if (Notification == null)
            {
                return NotFound();
            }

            // Lấy danh sách loại thông báo
            NotificationTypes = await _context.Notificationtypes.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                NotificationTypes = await _context.Notificationtypes.ToListAsync();
                return Page();
            }

            bool isUpdated = await _notificationService.UpdateNotificationAsync(Notification);
            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật thông báo!");
                return Page();
            }

            return Redirect("/Notification/Index");

        }
    }
}
