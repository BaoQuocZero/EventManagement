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
    // Create
    public async Task<bool> AddEventAsync(Event newEvent)
    {
        try
        {
            newEvent.CreateAt = DateTime.Now;
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi thêm sự kiện: " + ex.Message);
            return false;
        }
    }
    // Lấy danh sách tất cả loại sự kiện từ DB
    public async Task<List<Eventtype>> GetEventTypesAsync()
    {
        return await _context.Eventtypes.ToListAsync();
    }

}
