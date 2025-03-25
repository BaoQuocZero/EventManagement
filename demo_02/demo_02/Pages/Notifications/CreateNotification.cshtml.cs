using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace demo_02.Pages.Notifications
{
    public class CreateNotificationModel : PageModel
    {
        private readonly EventManagementContext _context;

        public CreateNotificationModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Notification Notification { get; set; }

        public List<Notificationtype> NotificationType { get; set; } // Danh sách loại thông báo

        public async Task<IActionResult> OnGetAsync()
        {
            NotificationType = await _context.Notificationtypes.ToListAsync(); // Lấy danh sách loại thông báo
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                NotificationType = await _context.Notificationtypes.ToListAsync(); // Load lại dữ liệu nếu có lỗi
                return Page();
            }
            Notification.CreateAt = DateTime.Now;

            _context.Notifications.Add(Notification);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
