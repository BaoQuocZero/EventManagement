using demo_02.Models;
using demo_02.Servers;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    }
}
