using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using demo_02.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace demo_02.Pages
{
    public class Index : PageModel
    {
        private readonly EventManagementContext _context;
        public string SessionUserId { get; set; }
        // Lưu JSON thay vì danh sách object để khỏi phải khai báo class
        public int TotalEvents { get; set; }
        public int TotalParticipants { get; set; }
        public string TopEvent { get; set; }
        public string TopDonatedEventsJson { get; set; }
        public string MonthlyStatsJson { get; set; } // JSON cho biểu đồ
        public Dictionary<string, int> EventTypesData { get; set; }
        public Dictionary<string, int> StatusData { get; set; }
        public Index(EventManagementContext context)
        {
            _context = context;
        }
        public class MonthlyStats // Tạo một class để hợp nhất dữ liệu
        {
            public int Year { get; set; }
            public int Month { get; set; }
            public int ParticipationCount { get; set; }
            public int TotalDonationAmount { get; set; }
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

            var topDonatedEvents = await _context.Events
                .Where(e => e.EndTime > DateTime.Now && e.IsDelete == false)
                .Select(e => new
                {
                    EventName = e.EventName,
                    TotalDonation = _context.Eventdonations
                        .Where(ed => ed.Participation.EventsId == e.EventsId && ed.IsDelete == false)
                        .Sum(ed => (int?)ed.Amount) ?? 0
                })
                .OrderByDescending(e => e.TotalDonation)
                .Take(5)
                .ToListAsync();

            // Chuyển đổi dữ liệu thành JSON để dùng trong Razor Page
            TopDonatedEventsJson = JsonConvert.SerializeObject(topDonatedEvents);


            // Lấy danh sách tất cả các tháng trong khoảng minDate -> maxDate
            var minDate = await _context.Eventparticipations
                .MinAsync(ep => ep.ParticipationTime ?? DateTime.MinValue);

            var maxDate = await _context.Eventparticipations
                .MaxAsync(ep => ep.ParticipationTime ?? DateTime.MaxValue);

            // Lấy danh sách tất cả các tháng từ minDate -> maxDate
            var allMonths = Enumerable.Range(0, ((maxDate.Year - minDate.Year) * 12) + maxDate.Month - minDate.Month + 1)
                .Select(i => new DateTime(minDate.Year, minDate.Month, 1).AddMonths(i))
                .Select(d => new MonthlyStats
                {
                    Year = d.Year,
                    Month = d.Month,
                    ParticipationCount = 0,
                    TotalDonationAmount = 0
                })
                .ToList();

            var participationData = await _context.Eventparticipations
                .Where(ep => ep.ParticipationTime.HasValue) // Bỏ qua nếu giá trị null
                .GroupBy(ep => new { Year = ep.ParticipationTime.Value.Year, Month = ep.ParticipationTime.Value.Month })
                .Select(g => new MonthlyStats
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    ParticipationCount = g.Count(),
                    TotalDonationAmount = 0
                })
                .ToListAsync();

            var donationData = await _context.Eventdonations
                .Where(ed => ed.DonationDate.HasValue) // Bỏ qua nếu giá trị null
                .GroupBy(ed => new { Year = ed.DonationDate.Value.Year, Month = ed.DonationDate.Value.Month })
                .Select(g => new MonthlyStats
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    ParticipationCount = 0,
                    TotalDonationAmount = (int)g.Sum(ed => ed.Amount)
                })
                .ToListAsync();

            // Gộp dữ liệu tham gia & quyên góp vào danh sách hoàn chỉnh
            var mergedData = allMonths
                .Concat(participationData)
                .Concat(donationData)
                .GroupBy(d => new { d.Year, d.Month })
                .Select(g => new MonthlyStats
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    ParticipationCount = g.Sum(d => d.ParticipationCount),
                    TotalDonationAmount = g.Sum(d => d.TotalDonationAmount)
                })
                .OrderBy(d => d.Year)
                .ThenBy(d => d.Month)
                .ToList();

            MonthlyStatsJson = System.Text.Json.JsonSerializer.Serialize(mergedData);

            return Page();
        }
    }
}