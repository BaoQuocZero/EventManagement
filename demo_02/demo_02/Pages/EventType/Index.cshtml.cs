using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace demo_02.Pages.EventTypes
{
    public class IndexModel : PageModel
    {
        private readonly EventService _eventService;

        public IndexModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public List<Eventtype> EventTypes { get; set; } = new List<Eventtype>();

        public async Task OnGetAsync()
        {
            EventTypes = await _eventService.GetAllEventTypesAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var success = await _eventService.DeleteEventTypeAsync(id);
            return success ? new JsonResult(new { success = true }) : NotFound();
        }
    }
}
