using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Notifications
{
    public class IndexNotificationModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public IndexNotificationModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public List<Notification> Notifications { get; set; } = new List<Notification>();

        public async Task OnGetAsync()
        {
            Notifications = await _notificationService.GetAllNotificationsAsync();
        }

        [BindProperty]
        public int? DeleteId { get; set; } // Lưu ID thông báo cần xóa



        public async Task<IActionResult> OnPostAsync()
        {
            if (DeleteId.HasValue)
            {
                var success = await _notificationService.DeleteNotificationAsync(DeleteId.Value);
                if (!success)
                {
                    ModelState.AddModelError("", "Không thể xóa.");
                }
            }

            return RedirectToPage();
        }
    }
}
