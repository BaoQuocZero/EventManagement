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

        public List<Event> Events { get; set; } = new List<Event>();

        public async Task OnGetAsync()
        {
            Events = await _eventsService.GetAllEventsAsync();
        }

        [BindProperty]
        public int? DeleteId { get; set; } // Lưu ID sự kiện cần xóa

        public async Task<IActionResult> OnPostAsync()
        {
            if (DeleteId.HasValue)
            {
                var success = await _eventsService.DeleteEventAsync(DeleteId.Value);
                if (!success)
                {
                    ModelState.AddModelError("", "Không thể xóa sự kiện.");
                }
            }

            return RedirectToPage();
        }
    }
}
