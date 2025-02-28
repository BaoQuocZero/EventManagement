using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class NotificationsModel : PageModel
{
    private readonly EventManagementContext _context;

    public NotificationsModel(EventManagementContext context)
    {
        _context = context;
    }

    public List<Notification> Notifications { get; set; } = new();

    public async Task OnGetAsync()
    {
        Notifications = await _context.Notifications
            .Include(n => n.Notificationtypes)
            .Where(n => n.IsDelete == false || n.IsDelete == null) // Lọc thông báo chưa bị xóa
            .OrderByDescending(n => n.CreateAt) // Sắp xếp theo ngày tạo
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var notification = await _context.Notifications.FindAsync(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
