using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_02.Models;
using demo_02.Servers;

namespace demo_02.Pages.NotificationType
{
    public class CreateNotificationTypeModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public CreateNotificationTypeModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public Notificationtype NotificationType { get; set; } = new Notificationtype();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isSuccess = await _notificationService.AddNotificationTypeAsync(NotificationType);
            if (isSuccess)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", "Thêm loại thông báo thất bại.");
            return Page();
        }
    }
}
