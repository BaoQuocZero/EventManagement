using demo_02.Models;
using Microsoft.EntityFrameworkCore;

public class EventsService
{
    private readonly EventManagementContext _context;

    public EventsService(EventManagementContext context)
    {
        _context = context;
    }

    // Lấy tất cả sự kiện (không lấy sự kiện bị xóa)
    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _context.Events
            .Where(e => e.IsDelete == false || e.IsDelete == null)
            .Include(e => e.Eventtypes) // Lấy luôn thông tin loại sự kiện
            .ToListAsync();
    }

    // Lấy chi tiết một sự kiện theo ID
    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        return await _context.Events
            .Where(e => e.EventsId == eventId && (e.IsDelete == false || e.IsDelete == null))
            .Include(e => e.Eventtypes) // Lấy luôn thông tin loại sự kiện
            .FirstOrDefaultAsync();
    }
}
