using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Events
{
    public class PreviewModel : PageModel
    {
        private readonly EventService _eventsService;

        public PreviewModel(EventService eventsService)
        {
            _eventsService = eventsService;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _eventsService.GetEventByIdAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
