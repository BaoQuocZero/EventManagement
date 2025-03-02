namespace demo_02.Models;

public partial class UserNotification
{
    public int NotificationsId { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Notification Notifications { get; set; }

    public virtual User User { get; set; }
}
