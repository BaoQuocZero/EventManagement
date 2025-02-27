using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventsService;

        public IndexModel(EventService eventsService)
        {
            _eventsService = eventsService;
        }

        public List<Event> Events { get; set; } = new List<Event>(); // Khởi tạo danh sách rỗng để tránh lỗi NullReference

        public async Task OnGetAsync()
        {
            Events = await _eventsService.GetAllEventsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _eventsService.DeleteEventAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./Index"); // Reload danh sách sự kiện
        }

    }
}
