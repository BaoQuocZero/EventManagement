using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace demo_02.Pages
{
    public class Index : PageModel
    {
        public string SessionUserId { get; set; }

        public IActionResult OnGet()
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
