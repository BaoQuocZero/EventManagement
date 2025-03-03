using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;
using System.Threading.Tasks;

public class NotificationDetailsModel : PageModel
{
    private readonly EventManagementContext _context;

    public NotificationDetailsModel(EventManagementContext context)
    {
        _context = context;
    }

    // C?n khai báo là public ?? Razor Page có th? truy c?p
    public Notification Notification { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Notification = await _context.Notifications
            .Include(n => n.Notificationtypes)
            .FirstOrDefaultAsync(n => n.NotificationsId == id);

        if (Notification == null)
        {
            return NotFound();
        }

        return Page();
    }
}
