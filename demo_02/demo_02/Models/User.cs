using System;
using System.Collections.Generic;

namespace demo_02.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RolesId { get; set; }

    public string StudentId { get; set; }

    public string FullName { get; set; }

    public string Classid { get; set; }

    public string Classname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public string Password { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<Eventparticipation> Eventparticipations { get; set; } = new List<Eventparticipation>();

    public virtual Role Roles { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
}
