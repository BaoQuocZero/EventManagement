using demo_02.Models;
using Microsoft.EntityFrameworkCore;

public class EventService
{
    private readonly EventManagementContext _context;

    public EventService(EventManagementContext context)
    {
        _context = context;
    }

    public async Task<List<Eventtype>> GetEventTypesAsync()
    {
        return await _context.Eventtypes
            .Where(e => e.IsDelete == false)
            .ToListAsync();
    }

    // GetAll
    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _context.Events
            .Where(e => e.IsDelete == false || e.IsDelete == null)
            .Include(e => e.Eventtypes)
            .ToListAsync();
    }

    // GetByID Event, details đang dùng
    public async Task<Event> GetEventByIdAsync(int eventId)
    {
        var eventData = await _context.Events
            .Where(e => e.EventsId == eventId && (e.IsDelete == false || e.IsDelete == null))
            .Include(e => e.Eventtypes)
            .Include(e => e.Eventparticipations)
            .ThenInclude(p => p.Eventdonations)
            .FirstOrDefaultAsync();

        if (eventData != null)
        {
            // Chỉ tính những người có trạng thái "Đã tham dự"
            eventData.TotalParticipants = eventData.Eventparticipations
                .Count(p => (p.IsDelete == false || p.IsDelete == null));

            // Tổng tiền donate của những người tham dự
            eventData.TotalDonations = eventData.Eventparticipations
                .Where(p => (p.IsDelete == false || p.IsDelete == null))
                .SelectMany(p => p.Eventdonations)
                .Where(d => d.IsDelete == false || d.IsDelete == null)
                .Sum(d => d.Amount ?? 0);

            // Số người vắng mặt
            eventData.AbsentCount = eventData.Eventparticipations
                .Count(p => (p.IsDelete == false || p.IsDelete == null) && p.ParticipationStatus == "Vắng");

            // Tính tỷ lệ tham gia
            if (eventData.MaxParticipants.HasValue && eventData.TotalParticipants > 0)
            {
                eventData.ParticipationRate = ((decimal)eventData.TotalParticipants - eventData.AbsentCount) / (decimal)eventData.TotalParticipants;
            }
            else
            {
                eventData.ParticipationRate = 0;
            }
        }

        return eventData;
    }

    //Update Event 
    public async Task<bool> UpdateEventAsync(Event updatedEvent)
    {
        var existingEvent = await _context.Events.FindAsync(updatedEvent.EventsId);
        if (existingEvent == null)
        {
            return false; // Không tìm thấy sự kiện
        }

        existingEvent.EventName = updatedEvent.EventName;
        existingEvent.EventtypesId = updatedEvent.EventtypesId;
        existingEvent.EventTime = updatedEvent.EventTime;
        existingEvent.EndTime = updatedEvent.EndTime;
        existingEvent.Location = updatedEvent.Location;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.RequiredParticipants = updatedEvent.RequiredParticipants;
        existingEvent.MaxParticipants = updatedEvent.MaxParticipants;
        existingEvent.DressCode = updatedEvent.DressCode;
        existingEvent.EventGroupLink = updatedEvent.EventGroupLink;
        existingEvent.AttendanceListLink = updatedEvent.AttendanceListLink;
        existingEvent.UpdateAt = DateTime.UtcNow;

        _context.Events.Update(existingEvent);
        await _context.SaveChangesAsync();

        return true;
    }

    //Delete
    public async Task<bool> DeleteEventAsync(int eventId)
    {
        var existingEvent = await _context.Events.FindAsync(eventId);
        if (existingEvent == null)
        {
            return false; // Không tìm thấy sự kiện
        }

        // Đánh dấu đã xóa thay vì xóa vĩnh viễn
        existingEvent.IsDelete = true;
        existingEvent.UpdateAt = DateTime.Now; // Ghi lại thời gian cập nhật

        await _context.SaveChangesAsync();
        return true; // Xóa thành công
    }

    // 1️⃣ Lấy danh sách tất cả người dùng (chưa bị xóa)
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users
            .Where(u => u.IsDelete == false || u.IsDelete == null)
            .ToListAsync();
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }


    // Thêm người dùng mới
    public async Task<bool> CreateUserAsync(User user)
    {
        try
        {
            user.CreateAt = DateTime.Now;
            user.IsDelete = false;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    // 2️⃣ Tìm kiếm người dùng theo ID
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users
            .Where(u => u.UserId == userId && (u.IsDelete == false || u.IsDelete == null))
            .Include(u => u.Roles)
            .FirstOrDefaultAsync();
    }


    // 4️⃣ Cập nhật thông tin người dùng
    public async Task<bool> UpdateUserAsync(User updatedUser)
    {
        var existingUser = await _context.Users.FindAsync(updatedUser.UserId);
        if (existingUser == null) return false;

        existingUser.FullName = !string.IsNullOrWhiteSpace(updatedUser.FullName) ? updatedUser.FullName : existingUser.FullName;
        existingUser.Email = !string.IsNullOrWhiteSpace(updatedUser.Email) ? updatedUser.Email : existingUser.Email;
        existingUser.PhoneNumber = !string.IsNullOrWhiteSpace(updatedUser.PhoneNumber) ? updatedUser.PhoneNumber : existingUser.PhoneNumber;
        existingUser.Classid = !string.IsNullOrWhiteSpace(updatedUser.Classid) ? updatedUser.Classid : existingUser.Classid;
        existingUser.Classname = !string.IsNullOrWhiteSpace(updatedUser.Classname) ? updatedUser.Classname : existingUser.Classname;
        existingUser.UpdateAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    // 5️⃣ Xóa mềm người dùng (ẩn đi)
    public async Task<bool> SoftDeleteUserAsync(int userId)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null) return false;

        existingUser.IsDelete = true;
        existingUser.UpdateAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return true;
    }

    // 6️⃣ Xóa cứng người dùng (Xóa hoàn toàn)
    public async Task<bool> HardDeleteUserAsync(int userId)
    {
        var existingUser = await _context.Users
            .Include(u => u.Eventparticipations)
            .Include(u => u.UserNotifications)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (existingUser == null) return false;

        // Xóa tất cả dữ liệu liên quan trước khi xóa user
        _context.Eventparticipations.RemoveRange(existingUser.Eventparticipations);
        _context.UserNotifications.RemoveRange(existingUser.UserNotifications);

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return true;
    }
    //Update Role & NameClass
    public async Task<bool> UpdateUserRoleAndClassAsync(int userId, int roleId, string classId, string className, string fullName)
    {
        try
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null) return false;

            // Kiểm tra roleId có tồn tại không
            var roleExists = await _context.Roles.AnyAsync(r => r.RolesId == roleId);
            if (!roleExists) return false; // Nếu quyền không hợp lệ, không cập nhật

            // Kiểm tra classId & className không rỗng
            if (string.IsNullOrWhiteSpace(classId) || string.IsNullOrWhiteSpace(className))
                return false;

            existingUser.RolesId = roleId;
            existingUser.Classid = classId;
            existingUser.Classname = className;
            existingUser.FullName = fullName;
            existingUser.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi cập nhật quyền & lớp học: {ex.Message}");
            return false;
        }
    }

    //Tham gia sự kiện
    //Get All Eventparticipation(tham gia sự kiện)
    public async Task<List<Eventparticipation>> GetAllEventParticipationsAsync()
    {
        return await _context.Eventparticipations
            .Where(ep => ep.IsDelete == false || ep.IsDelete == null)
            .Include(ep => ep.Events)  // Lấy thông tin sự kiện
            .Include(ep => ep.User)    // Lấy thông tin người dùng
            .ToListAsync();
    }

    //Phân trang
    // Lấy danh sách EventParticipations với phân trang
    // Lấy danh sách EventParticipations có phân trang
    public async Task<List<Eventparticipation>> GetEventParticipationsPaginatedAsync(int page, int pageSize)
    {
        return await _context.Eventparticipations
            .Include(e => e.Events)
            .Include(e => e.User)
            .OrderBy(e => e.ParticipationId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // Lấy danh sách EventParticipations dưới dạng IQueryable để dùng với DevExtreme DataGrid
    public IQueryable<Eventparticipation> GetEventParticipations()
    {
        return _context.Eventparticipations
            .Include(e => e.Events)
            .Include(e => e.User);
    }

    // Xóa một bản ghi EventParticipation
    public async Task<bool> DeleteEventparticipationAsync(int id)
    {
        var participation = await _context.Eventparticipations.FindAsync(id);
        if (participation == null) return false;

        _context.Eventparticipations.Remove(participation);
        await _context.SaveChangesAsync();
        return true;
    }


    //Detail Eventparticipation
    public async Task<Eventparticipation> GetEventParticipationByIdAsync(int participationId)
    {
        return await _context.Eventparticipations
            .Where(ep => ep.ParticipationId == participationId)  
            .Include(ep => ep.Events)  
            .Include(ep => ep.User)  
            .FirstOrDefaultAsync();
    }

    //Delete Eventparticipation
    //public async Task<bool> DeleteEventparticipationAsync(int eventId)
    //{
    //    var existingEvent = await _context.Eventparticipations.FindAsync(eventId);
    //    if (existingEvent == null)
    //    {
    //        return false; // Không tìm thấy sự kiện
    //    }

    //    // Thay vì xóa hoàn toàn, đặt cờ IsDelete để có thể khôi phục sau này
    //    existingEvent.IsDelete = true;
    //    existingEvent.UpdateAt = DateTime.Now; // Ghi lại thời gian cập nhật

    //    await _context.SaveChangesAsync();
    //    return true; // Xóa thành công
    //}

    // Cập nhật thông tin tham gia sự kiện
    public async Task<bool> UpdateEventParticipationAsync(Eventparticipation updatedParticipation)
    {
        var existingParticipation = await _context.Eventparticipations.FindAsync(updatedParticipation.ParticipationId);
        if (existingParticipation == null)
        {
            return false; // Không tìm thấy dữ liệu
        }

        // Cập nhật thông tin nếu có giá trị mới
        existingParticipation.ParticipationStatus = !string.IsNullOrWhiteSpace(updatedParticipation.ParticipationStatus)
            ? updatedParticipation.ParticipationStatus
            : existingParticipation.ParticipationStatus;

        existingParticipation.EarnedPoints = updatedParticipation.EarnedPoints ?? existingParticipation.EarnedPoints;
        existingParticipation.ParticipationTime = updatedParticipation.ParticipationTime ?? existingParticipation.ParticipationTime;
        existingParticipation.UpdateAt = DateTime.Now; // Cập nhật thời gian sửa đổi

        await _context.SaveChangesAsync();
        return true; // Cập nhật thành công
    }
    // 🆕 Đếm tổng số sự kiện (chỉ tính những sự kiện chưa bị xóa)
    public async Task<int> CountTotalEventsAsync()
    {
        return await _context.Events.CountAsync(e => e.IsDelete == false || e.IsDelete == null);
    }
    //Lấy chi tiết User
    public async Task<List<Event>> GetEventsByUserIdAsync(int userId)
    {
        return await _context.Eventparticipations
            .Where(p => p.UserId == userId) // Chỉ lấy các sự kiện mà người dùng đã tham gia
            .Include(p => p.Events) // Bao gồm thông tin sự kiện
            .Select(p => p.Events) // Chỉ lấy danh sách sự kiện
            .ToListAsync();
    }
    // Thống kê điểm và tiền donate User
    public async Task<int> GetTotalPointsByUserIdAsync(int userId)
    {
        return await _context.Eventparticipations
            .Where(p => p.UserId == userId) // Lọc theo UserId
            .SumAsync(p => p.EarnedPoints ?? 0); // Tính tổng điểm, bỏ qua null
    }
    public async Task<int> GetTotalDonationsByUserIdAsync(int userId)
    {
        return await _context.Eventdonations
            .Where(d => _context.Eventparticipations
                .Where(p => p.UserId == userId) // Lọc theo UserId
                .Select(p => p.ParticipationId)
                .Contains(d.ParticipationId)) // Lọc theo ParticipationId
            .SumAsync(d => d.Amount ?? 0); // Tính tổng số tiền, bỏ qua null
    }


    // Xử lý Loại Sự kiện (EventTypes)
    // Thêm mới loại sự kiện với isDelete = false và ngày tạo là hiện tại
    public async Task<bool> AddEventTypeAsync(Eventtype eventType)
    {
        eventType.IsDelete = false; // Không bị xoá mềm
        eventType.CreateAt = DateTime.Now;
        eventType.UpdateAt = null;

        _context.Eventtypes.Add(eventType);
        return await _context.SaveChangesAsync() > 0;
    }

    // Lấy loại sự kiện theo ID (chỉ lấy những cái chưa bị xóa)
    public async Task<Eventtype> GetEventTypeByIdAsync(int id)
    {
        return await _context.Eventtypes
            .Where(et => et.IsDelete == false)
            .FirstOrDefaultAsync(et => et.EventtypesId == id);
    }

    // Cập nhật loại sự kiện
    public async Task<bool> UpdateEventTypeAsync(Eventtype eventType)
    {
        var existingEventType = await _context.Eventtypes.FindAsync(eventType.EventtypesId);
        if (existingEventType == null || existingEventType.IsDelete == true)
            return false;

        existingEventType.EventtypesName = eventType.EventtypesName;
        existingEventType.UpdateAt = DateTime.Now;

        _context.Eventtypes.Update(existingEventType);
        return await _context.SaveChangesAsync() > 0;
    }

    // Xóa mềm loại sự kiện (IsDelete = true, cập nhật UpdateAt)
    public async Task<bool> DeleteEventTypeAsync(int id)
    {
        var eventType = await _context.Eventtypes.FindAsync(id);
        if (eventType == null)
        {
            return false; // Không tìm thấy
        }

        eventType.IsDelete = true;
        eventType.UpdateAt = DateTime.Now;// Đánh dấu là đã xóa (ẩn đi)
        await _context.SaveChangesAsync();
        return true;
    }


    // Lấy tất cả các loại sự kiện chưa bị xóa
    public async Task<List<Eventtype>> GetAllEventTypesAsync()
    {
        return await _context.Eventtypes
            .Where(et => et.IsDelete == false)
            .ToListAsync();
    }
}
