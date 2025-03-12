using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_02.Models;
using demo_02.Servers;
using System.Threading.Tasks;

namespace demo_02.Pages.NotificationType
{
    public class EditModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public EditModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public Notificationtype NotificationType { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            NotificationType = await _notificationService.GetNotificationTypeByIdAsync(id);

            if (NotificationType == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool updated = await _notificationService.UpdateNotificationTypeAsync(NotificationType);

            if (!updated)
            {
                return NotFound();
            }

            return RedirectToPage("Index");
        }
    }
}
