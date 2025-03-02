using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.Eventparticipations
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _eventsService;

        public DetailsModel(EventService eventsService)
        {
            _eventsService = eventsService;
        }

        public Eventparticipation EventParticipationDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EventParticipationDetail = await _eventsService.GetEventParticipationByIdAsync(id);

            if (EventParticipationDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
