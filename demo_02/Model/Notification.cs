using System;
using System.Collections.Generic;

namespace demo_02.Model;

public partial class Notification
{
    public int NotificationsId { get; set; }

    public int NotificationtypesId { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public string Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Notificationtype Notificationtypes { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
