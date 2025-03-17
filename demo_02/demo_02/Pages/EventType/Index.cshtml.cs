using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_02.Pages.EventTypes
{
    public class IndexModel : PageModel
    {
        private readonly EventManagementContext _context;

        public IndexModel(EventManagementContext context)
        {
            _context = context;
        }

        public List<Eventtype> EventTypes { get; set; } = new List<Eventtype>();

        [TempData]
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            EventTypes = await _context.Eventtypes
                .Where(e => e.IsDelete == false) // Lọc bỏ các bản ghi đã bị xóa mềm
                .ToListAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var eventType = await _context.Eventtypes.FindAsync(id);
            if (eventType == null)
            {
                Message = "Loại sự kiện không tồn tại!";
                return RedirectToPage();
            }

            eventType.IsDelete = true;
            eventType.UpdateAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                Message = "Xóa loại sự kiện thành công!";
            }
            catch
            {
                Message = "Lỗi khi xóa sự kiện!";
            }

            return RedirectToPage();
        }
    }
}
