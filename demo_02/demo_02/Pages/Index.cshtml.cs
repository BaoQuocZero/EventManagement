using System;
using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging; // Import thư viện logging
using Microsoft.EntityFrameworkCore;


public class IndexModel : PageModel
{
    private readonly EventManagementContext _context;
    private readonly ILogger<IndexModel> _logger; // Thêm Logger

    public IndexModel(EventManagementContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger; // Inject logger
    }

    public string EventsJson { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var events = await _context.Events
                .Include(e => e.Eventtypes)
                .Where(e => e.IsDelete == false || e.IsDelete == null)
                .Select(e => new EventDTO
                {
                    EventsId = e.EventsId,
                    EventName = e.EventName,
                    EventTime = e.EventTime,
                    EndTime = e.EndTime,
                    Location = e.Location,
                    Description = e.Description,
                    RequiredParticipants = e.RequiredParticipants,
                    MaxParticipants = e.MaxParticipants,
                    EventTypeName = e.Eventtypes != null ? e.Eventtypes.EventtypesName : "Không có loại sự kiện"
                })
                .ToListAsync();

            _logger.LogInformation($"Tổng số sự kiện: {events.Count}"); // Ghi log số lượng sự kiện lấy được

            if (events.Count == 0)
            {
                EventsJson = "[]"; // Tránh lỗi null
            }
            else
            {
                EventsJson = JsonSerializer.Serialize(events, new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }

            _logger.LogInformation($"JSON Data: {EventsJson}"); // Log JSON

            return Page();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Lỗi khi lấy dữ liệu sự kiện: {ex.Message}");
            return StatusCode(500, "Lỗi server khi lấy dữ liệu sự kiện");
        }
    }
}
