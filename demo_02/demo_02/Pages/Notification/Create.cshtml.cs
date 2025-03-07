using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_02.Pages.Notification
{
    public class CreateModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public CreateModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public demo_02.Models.Notification Notification { get; set; } = new demo_02.Models.Notification();

        [BindProperty]
        public List<Notificationtype> NotificationTypes { get; set; } = new List<Notificationtype>();
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Notification.CreateAt = DateTime.UtcNow;
            bool isSuccess = await _notificationService.CreateNotificationAsync(Notification);

            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tạo thông báo!");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
