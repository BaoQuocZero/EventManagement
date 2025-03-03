using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class EditNotificationModel : PageModel
{
    private readonly EventManagementContext _context;

    public EditNotificationModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Notification Notification { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Notification = await _context.Notifications.FindAsync(id);
        if (Notification == null)
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

        _context.Attach(Notification).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return RedirectToPage("Notifications");
    }
}
