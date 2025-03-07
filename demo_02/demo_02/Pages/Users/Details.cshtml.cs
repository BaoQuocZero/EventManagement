using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace demo_02.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _eventService;

        public DetailsModel(EventService eventService)
        {
            _eventService = eventService;
        }

        public User CurrentUser { get; set; }

        // Thống kê
        public int TotalEvents { get; set; } // Tổng số sự kiện
        public int ParticipatedEvents { get; set; } // Sự kiện đã tham gia
        public int TotalDonations { get; set; } // Tổng số tiền donate
        public int TotalPoints { get; set; } // Tổng điểm kiếm được
        public double ParticipationRate { get; set; } // Tỷ lệ tham gia (%)

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Lấy thông tin người dùng
            CurrentUser = await _eventService.GetUserByIdAsync(id);

            if (CurrentUser == null)
            {
                return NotFound();
            }

            // Lấy danh sách sự kiện mà người dùng đã tham gia
            var participations = await _eventService.GetEventsByUserIdAsync(id);

            // Tính toán thống kê
            TotalEvents = await _eventService.CountTotalEventsAsync();
            ParticipatedEvents = participations.Count;
            TotalPoints = await _eventService.GetTotalPointsByUserIdAsync(id);
            TotalDonations = await _eventService.GetTotalDonationsByUserIdAsync(id);
            ParticipationRate = TotalEvents > 0 ? (double)ParticipatedEvents / TotalEvents * 100 : 0;

            return Page();
        }

    }
}
