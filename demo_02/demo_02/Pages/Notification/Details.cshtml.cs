using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace demo_02.Pages.Notification
{
    public class DetailsModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public DetailsModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public demo_02.Models.Notification Notification { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Notification = await _notificationService.GetNotifications()
                .FirstOrDefaultAsync(n => n.NotificationsId == id);

            if (Notification == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
