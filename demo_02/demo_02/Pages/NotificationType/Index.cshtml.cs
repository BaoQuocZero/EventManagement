using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_02.Pages.NotificationType
{
    public class IndexModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public IndexModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public List<Notificationtype> NotificationTypes { get; set; }

        public async Task OnGetAsync()
        {
            NotificationTypes = await _notificationService.GetAllNotificationTypesAsync();
        }

        [BindProperty]
        public int? DeleteId { get; set; } // Lưu ID thông báo cần xóa

        public async Task<IActionResult> OnPostAsync()
        {
            if (DeleteId.HasValue)
            {
                var success = await _notificationService.DeleteNotificationTypeAsync(DeleteId.Value);
                if (!success)
                {
                    ModelState.AddModelError("", "Không thể xóa.");
                }
            }

            return RedirectToPage();
        }
    }
}
