using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace demo_02.Pages.EventParticipations
{
    public class EditModel : PageModel
    {
        private readonly EventService _eventService;

        public EditModel(EventService eventService)
        {
            _eventService = eventService;
        }

        [BindProperty]
        public Eventparticipation EventParticipation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EventParticipation = await _eventService.GetEventParticipationByIdAsync(id);

            if (EventParticipation == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Trả về trang để hiển thị lỗi
            }

            // Kiểm tra điểm hợp lệ
            if (EventParticipation.EarnedPoints < 0 || EventParticipation.EarnedPoints > 10)
            {
                ModelState.AddModelError("EventParticipation.EarnedPoints", "Điểm phải nằm trong khoảng 0 - 10.");
                return Page();
            }

            var success = await _eventService.UpdateEventParticipationAsync(EventParticipation);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Cập nhật thất bại. Vui lòng thử lại.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
