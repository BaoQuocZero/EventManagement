using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Notifications
{
    public class EditNotificationModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public EditNotificationModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public Notification Notification { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Notification = await _notificationService.GetNotificationByIdAsync(id);

            if (Notification == null)
            {
                return NotFound();
            }

            // Lấy danh sách loại thông báo và truyền vào ViewData
            ViewData["NotificationTypes"] = await _notificationService.GetNotificationTypesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Notification.UpdateAt = DateTime.Now;

            bool isUpdated = await _notificationService.UpdateNotificationAsync(Notification);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToPage("IndexNotification"); // Quay về trang danh sách thông báo
        }
    }
}
