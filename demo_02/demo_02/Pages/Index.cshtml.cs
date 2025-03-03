using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace demo_02.Pages
{
    public class Index : PageModel
    {
        private readonly EventManagementContext _context;
        public string SessionUserId { get; set; }

        public int TotalEvents { get; set; }
        public int TotalParticipants { get; set; }
        public string TopEvent { get; set; }
        public Dictionary<string, int> EventTypesData { get; set; }
        public Dictionary<string, int> StatusData { get; set; }
        public Dictionary<string, int> StudentData { get; set; }

        public Index(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SessionUserId = HttpContext.Session.GetString("UserId") ?? "Không có UserId";

            // Kiểm tra nếu chưa đăng nhập
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToPage("/Account/login"); // Chuyển hướng đến trang đăng nhập
            }
         
            return Page();
        }
    }
}