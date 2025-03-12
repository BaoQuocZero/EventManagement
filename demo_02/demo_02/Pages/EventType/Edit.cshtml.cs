using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using demo_02.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace demo_02.Pages.EventType
{
    public class EditModel : PageModel
    {
        private readonly EventManagementContext _context;

        public EditModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Eventtype EventType { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EventType = await _context.Eventtypes.FindAsync(id);

            if (EventType == null)
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

            var existingEventType = await _context.Eventtypes
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EventtypesId == EventType.EventtypesId);

            if (existingEventType == null)
            {
                return NotFound();
            }

            // ? Gi? nguyên các giá tr? quan tr?ng
            EventType.IsDelete = existingEventType.IsDelete;
            EventType.CreateAt = existingEventType.CreateAt;
            EventType.UpdateAt = DateTime.Now;

            _context.Attach(EventType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Eventtypes.Any(e => e.EventtypesId == EventType.EventtypesId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
        }
    }
}
