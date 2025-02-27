using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Events
{
    public class EditModel : PageModel
    {
        private readonly EventService _eventsService;

        public EditModel(EventService eventsService)
        {
            _eventsService = eventsService;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await _eventsService.GetEventByIdAsync(id);

            if (Event == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isUpdated = await _eventsService.UpdateEventAsync(Event);

            if (!isUpdated)
            {
                return NotFound();
            }

            return RedirectToPage("Index"); // Quay về trang danh sách sự kiện
        }
    }
}
