using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace demo_02.Pages.EventParticipations
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;

        public IndexModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Eventparticipation> EventParticipations { get; set; }

        public async Task OnGetAsync()
        {
            EventParticipations = await _eventService.GetAllEventParticipationsAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _eventService.DeleteEventparticipationAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

    }
}
