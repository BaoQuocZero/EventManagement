using System;
using System.Collections.Generic;

namespace demo_02.Models;

public partial class Notificationtype
{
    public int NotificationtypesId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
