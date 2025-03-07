using demo_02.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_02.Servers
{
    public class NotificationService
    {
        private readonly EventManagementContext _context;

        public NotificationService(EventManagementContext context)
        {
            _context = context;
        }

        // Lấy danh sách thông báo
        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .OrderByDescending(n => n.CreateAt)
                .ToListAsync();
        }

        // Lấy danh sách thông báo dưới dạng IQueryable (cho lọc/tìm kiếm)
        public IQueryable<Notification> GetNotifications()
        {
            return _context.Notifications.Include(n => n.Notificationtypes);
        }

        // Xóa thông báo
        public async Task<bool> DeleteNotificationAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null) return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        // Thêm mới thông báo
        public async Task<bool> CreateNotificationAsync(Notification notification)
        {
            if (notification == null) return false;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        // Chỉnh sửa thông báo
        public async Task<bool> UpdateNotificationAsync(Notification notification)
        {
            var existingNotification = await _context.Notifications.FindAsync(notification.NotificationsId);
            if (existingNotification == null) return false;

            existingNotification.Title = notification.Title;
            existingNotification.Message = notification.Message;
            existingNotification.NotificationtypesId = notification.NotificationtypesId;
            existingNotification.CreateAt = notification.CreateAt;

            _context.Notifications.Update(existingNotification);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
