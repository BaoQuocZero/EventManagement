using demo_02.Data;
using demo_02.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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

        public async Task OnGetAsync(int page = 1, int pageSize = 10)
        {
            EventParticipations = await _eventService.GetEventParticipationsPaginatedAsync(page, pageSize);

            // Kiểm tra xem dữ liệu có thực sự có không
            foreach (var item in EventParticipations)
            {
                Console.WriteLine($"ID: {item.ParticipationId}, Event: {item.Events?.EventName}, User: {item.User?.FullName}");
            }
        }


        public IActionResult OnGetData(DataSourceLoadOptions loadOptions)
        {
            var data = _eventService.GetEventParticipations()
                .Select(e => new EventParticipationDTO
                {
                    ParticipationId = e.ParticipationId,
                    EventName = e.Events.EventName,
                    UserName = e.User.FullName,
                    ParticipationStatus = e.ParticipationStatus,
                    ParticipationTime = e.ParticipationTime,
                    EarnedPoints = e.EarnedPoints
                });

            return new JsonResult(DataSourceLoader.Load(data, loadOptions));
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
