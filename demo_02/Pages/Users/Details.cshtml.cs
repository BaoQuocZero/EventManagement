using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _eventService;

        public DetailsModel(EventService eventService)
        {
            _eventService = eventService;
        }

        // Thuộc tính để hiển thị thông tin người dùng
        public User CurrentUser { get; set; }

        // Phương thức OnGetAsync để lấy dữ liệu
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Gọi UserService để lấy thông tin người dùng
            CurrentUser = await _eventService.GetUserByIdAsync(id);

            // Nếu không tìm thấy, trả về NotFound (hoặc bạn có thể điều hướng sang trang khác)
            if (CurrentUser == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
