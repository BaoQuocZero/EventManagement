using demo_02.Models;
using demo_02.Servers;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_02.Pages.Notification
{
    public class IndexModel : PageModel
    {
        private readonly NotificationService _notificationService;

        public IndexModel(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public List<demo_02.Models.Notification> Notifications { get; set; }

        public async Task OnGetAsync()
        {
            Notifications = await _notificationService.GetAllNotificationsAsync();
        }

        public IActionResult OnGetData(DataSourceLoadOptions loadOptions)
        {
            var data = _notificationService.GetNotifications()
                .Select(n => new
                {
                    n.NotificationsId,
                    n.Title,
                    n.Message,
                    NotificationType = n.Notificationtypes.Name,
                    n.Status,
                    n.CreateAt
                });

            return new JsonResult(DataSourceLoader.Load(data, loadOptions));
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _notificationService.DeleteNotificationAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
