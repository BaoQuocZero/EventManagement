using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class DetailsModel : PageModel
{
    private readonly EventsService _eventsService;

    public DetailsModel(EventsService eventsService)
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
