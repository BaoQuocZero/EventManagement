using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class CreateNotificationModel : PageModel
{
    private readonly EventManagementContext _context;

    public CreateNotificationModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Notification Notification { get; set; }

    public SelectList NotificationTypes { get; set; }

    public async Task OnGetAsync()
    {
        NotificationTypes = new SelectList(await _context.Notificationtypes.ToListAsync(), "NotificationtypesId", "Name");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Notification.CreateAt = DateTime.Now;
        _context.Notifications.Add(Notification);
        await _context.SaveChangesAsync();

        return RedirectToPage("Notifications");
    }
}
