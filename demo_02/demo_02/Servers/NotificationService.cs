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

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .OrderByDescending(n => n.CreateAt)
                .ToListAsync();
        }

        //public IQueryable<Notification> GetNotifications()
        //{
        //    return _context.Notifications.Include(n => n.Notificationtypes);
        //}



        // Xóa thông báo thành công
        public async Task<bool> DeleteNotificationAsync(int NotificationsId)
        {
            var existingNotification = await _context.Notifications.FindAsync(NotificationsId);
            if (existingNotification == null)
            {
                return false; // Không tìm thấy thông báo
            }

            _context.Notifications.Remove(existingNotification); // Xóa vĩnh viễn
            await _context.SaveChangesAsync();
            return true; // Xóa thành công
        }

        public async Task<Notification> GetNotificationByIdAsync(int NotificationsId)
        {
            var notification = await _context.Notifications
                .Where(n => n.NotificationsId == NotificationsId && (n.IsDelete == false || n.IsDelete == null))
                .Include(n => n.Notificationtypes)
                .FirstOrDefaultAsync();

            return notification;
        }

        // Lấy loại thông báo
        public async Task<List<Notificationtype>> GetNotificationTypesAsync()
        {
            return await _context.Notificationtypes
                .Where(n => n.IsDelete == false)
                .ToListAsync();
        }


        //public async Task<bool> CreateNotificationAsync(Notification notification)
        //{
        //    if (notification == null) return false;

        //    // Kiểm tra loại thông báo có tồn tại không
        //    var typeExists = await _context.Notificationtypes
        //        .AnyAsync(nt => nt.NotificationtypesId == notification.NotificationtypesId);
        //    if (!typeExists) return false;

        //    _context.Notifications.Add(notification);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}




        public async Task<bool> UpdateNotificationAsync(Notification notification)
        {
            var existingNotification = await _context.Notifications.FindAsync(notification.NotificationsId);
            if (existingNotification == null) return false;

            existingNotification.Title = notification.Title;
            existingNotification.Message = notification.Message;
            existingNotification.NotificationtypesId = notification.NotificationtypesId;
            existingNotification.Status = notification.Status;
            existingNotification.CreateAt = notification.CreateAt;
            existingNotification.UpdateAt = notification.UpdateAt;

            _context.Notifications.Update(existingNotification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Notificationtype>> GetAllNotificationTypesAsync()
        {
            return await _context.Notificationtypes
                .Where(nt => nt.IsDelete == false || nt.IsDelete == null)
                .OrderBy(nt => nt.Name)
                .ToListAsync();
        }
        public async Task<bool> AddNotificationTypeAsync(Notificationtype notificationType)
        {
            if (notificationType == null || string.IsNullOrWhiteSpace(notificationType.Name))
            {
                return false;
            }

            // Kiểm tra xem loại thông báo đã tồn tại chưa
            bool exists = await _context.Notificationtypes
                .AnyAsync(nt => nt.Name == notificationType.Name);

            if (exists)
            {
                return false; // Tránh trùng lặp loại thông báo
            }

            notificationType.IsDelete = false; // Mặc định không xóa
            _context.Notificationtypes.Add(notificationType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNotificationTypeAsync(Notificationtype notificationType)
        {
            var existingNotificationType = await _context.Notificationtypes
                .FindAsync(notificationType.NotificationtypesId);

            if (existingNotificationType == null) return false;

            existingNotificationType.Name = notificationType.Name;
            existingNotificationType.Description = notificationType.Description;
            existingNotificationType.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Notificationtype> GetNotificationTypeByIdAsync(int id)
        {
            return await _context.Notificationtypes
                .Where(nt => nt.NotificationtypesId == id && (nt.IsDelete == false || nt.IsDelete == null))
                .FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteNotificationTypeAsync(int NotificationtypesId)
        {
            var existingNotificationtype = await _context.Notificationtypes.FindAsync(NotificationtypesId);
            if (existingNotificationtype == null)
            {
                return false; // Không tìm thấy loại thông báo
            }

            _context.Notificationtypes.Remove(existingNotificationtype); // Xóa vĩnh viễn
            await _context.SaveChangesAsync();
            return true; // Xóa thành công
        }


    }
}
