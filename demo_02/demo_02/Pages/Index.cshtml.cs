using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using demo_02.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_02.Pages
{
    public class Index : PageModel
    {
        private readonly EventManagementContext _context;
        public string SessionUserId { get; set; }

        public int TotalEvents { get; set; }
        public int TotalParticipants { get; set; }
        public string TopEvent { get; set; }
        public Dictionary<string, int> EventTypesData { get; set; }
        public Dictionary<string, int> StatusData { get; set; }
        public Dictionary<string, int> StudentData { get; set; }
        public Index(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SessionUserId = HttpContext.Session.GetString("UserId") ?? "Không có UserId";

            //if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            //{
            //    return RedirectToPage("/Account/Login");
            //}

            // Thống kê tổng số sự kiện
            TotalEvents = await _context.Events.CountAsync();

            // Thống kê tổng số lượt tham gia
            TotalParticipants = await _context.Eventparticipations.CountAsync();

            // Sự kiện có nhiều người tham gia nhất
            TopEvent = await _context.Events
                .OrderByDescending(e => e.Eventparticipations.Count)
                .Select(e => e.EventName)
                .FirstOrDefaultAsync() ?? "Không có dữ liệu";

            // Thống kê loại sự kiện
            EventTypesData = await _context.Eventtypes
                .Select(et => new { et.EventtypesName, Count = et.Events.Count })
                .ToDictionaryAsync(e => e.EventtypesName, e => e.Count);

            // Thống kê trạng thái sự kiện
            StatusData = new Dictionary<string, int>
            {
                { "Đang mở", await _context.Events.Where(e => e.EventTime > DateTime.Now).CountAsync() },
                { "Sắp diễn ra", await _context.Events.Where(e => e.EventTime > DateTime.Now.AddDays(7)).CountAsync() },
                { "Đã kết thúc", await _context.Events.Where(e => e.EndTime < DateTime.Now).CountAsync() }
            };

            // Thống kê số lượng sinh viên tham gia theo tháng
            StudentData = await _context.Eventparticipations
                .GroupBy(ep => ep.ParticipationTime.Value.Month)
                .Select(g => new { Month = $"Tháng {g.Key}", Count = g.Count() })
                .ToDictionaryAsync(g => g.Month, g => g.Count);

            return Page();
        }
    }
}