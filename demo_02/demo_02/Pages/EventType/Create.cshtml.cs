using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_02.Models;
using System;
using System.Threading.Tasks;

namespace demo_02.Pages.EventType
{
    public class CreateModel : PageModel
    {
        private readonly EventManagementContext _context;

        public CreateModel(EventManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Eventtype EventType { get; set; } = new Eventtype();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ?? Gán giá tr? m?c ??nh khi t?o m?i
            EventType.IsDelete = false; // Không b? xóa
            EventType.CreateAt = DateTime.Now; // Ngày t?o hi?n t?i

            _context.Eventtypes.Add(EventType);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
