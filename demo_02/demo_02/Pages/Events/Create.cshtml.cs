using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;

public class CreateModel : PageModel
{
    private readonly EventManagementContext _context;

    public CreateModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Event Event { get; set; }

    public List<Eventtype> EventTypes { get; set; } // Danh sách loại sự kiện

    public async Task<IActionResult> OnGetAsync()
    {
        EventTypes = await _context.Eventtypes.ToListAsync(); // Lấy danh sách loại sự kiện
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            EventTypes = await _context.Eventtypes.ToListAsync(); // Load lại dữ liệu nếu có lỗi
            return Page();
        }

        _context.Events.Add(Event);
        await _context.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
