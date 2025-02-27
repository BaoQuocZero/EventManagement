using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EventsService _eventsService;

        public IndexModel(EventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public List<Event> Events { get; set; } = new List<Event>(); // Khởi tạo danh sách rỗng để tránh lỗi NullReference

        public async Task OnGetAsync()
        {
            Events = await _eventsService.GetAllEventsAsync();
        }
    }
}
