using Microsoft.EntityFrameworkCore;

namespace demo_02.Models;

public partial class EventManagementContext : DbContext
{
    public EventManagementContext()
    {
    }

    public EventManagementContext(DbContextOptions<EventManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventdonation> Eventdonations { get; set; }

    public virtual DbSet<Eventparticipation> Eventparticipations { get; set; }

    public virtual DbSet<Eventtype> Eventtypes { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Notificationtype> Notificationtypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6JRBMVT\\SQLEXPRESS;Initial Catalog=EventManagement;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventsId).IsClustered(false);

            entity.ToTable("EVENTS");

            entity.Property(e => e.EventsId).HasColumnName("EVENTS_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("END_TIME");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .HasColumnName("EVENT_NAME");
            entity.Property(e => e.EventTime)
                .HasColumnType("datetime")
                .HasColumnName("EVENT_TIME");
            entity.Property(e => e.EventtypesId).HasColumnName("EVENTTYPES_ID");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("LOCATION");
            entity.Property(e => e.MaxParticipants).HasColumnName("MAX_PARTICIPANTS");
            entity.Property(e => e.RequiredParticipants).HasColumnName("REQUIRED_PARTICIPANTS");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");

            entity.HasOne(d => d.Eventtypes).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventtypesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTS_EVENTS_EV_EVENTTYP");
        });

        modelBuilder.Entity<Eventdonation>(entity =>
        {
            entity.HasKey(e => e.EventdonationsId).IsClustered(false);

            entity.ToTable("EVENTDONATIONS");

            entity.Property(e => e.EventdonationsId).HasColumnName("EVENTDONATIONS_ID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.DonationDate)
                .HasColumnType("datetime")
                .HasColumnName("DONATION_DATE");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.ParticipationId).HasColumnName("PARTICIPATION_ID");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");

            entity.HasOne(d => d.Participation).WithMany(p => p.Eventdonations)
                .HasForeignKey(d => d.ParticipationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTDON_EVENTDONA_EVENTPAR");
        });

        modelBuilder.Entity<Eventparticipation>(entity =>
        {
            entity.HasKey(e => e.ParticipationId).IsClustered(false);

            entity.ToTable("EVENTPARTICIPATIONS");

            entity.Property(e => e.ParticipationId).HasColumnName("PARTICIPATION_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.EarnedPoints).HasColumnName("EARNED_POINTS");
            entity.Property(e => e.EventsId).HasColumnName("EVENTS_ID");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.ParticipationStatus)
                .HasMaxLength(60)
                .HasColumnName("PARTICIPATION_STATUS");
            entity.Property(e => e.ParticipationTime)
                .HasColumnType("datetime")
                .HasColumnName("PARTICIPATION_TIME");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Events).WithMany(p => p.Eventparticipations)
                .HasForeignKey(d => d.EventsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTPAR_EVENTPART_EVENTS");

            entity.HasOne(d => d.User).WithMany(p => p.Eventparticipations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EVENTPAR_EVENTPART_USERS");
        });

        modelBuilder.Entity<Eventtype>(entity =>
        {
            entity.HasKey(e => e.EventtypesId).IsClustered(false);

            entity.ToTable("EVENTTYPES");

            entity.Property(e => e.EventtypesId).HasColumnName("EVENTTYPES_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.EventtypesName)
                .HasMaxLength(1000)
                .HasColumnName("EVENTTYPES_NAME");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationsId).IsClustered(false);

            entity.ToTable("NOTIFICATIONS");

            entity.Property(e => e.NotificationsId).HasColumnName("NOTIFICATIONS_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.Message)
                .HasColumnType("ntext")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.NotificationtypesId).HasColumnName("NOTIFICATIONTYPES_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(60)
                .HasColumnName("STATUS");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");

            entity.HasOne(d => d.Notificationtypes).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationtypesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICA_NOTIFICAT_NOTIFICA");
        });

        modelBuilder.Entity<Notificationtype>(entity =>
        {
            entity.HasKey(e => e.NotificationtypesId).IsClustered(false);

            entity.ToTable("NOTIFICATIONTYPES");

            entity.Property(e => e.NotificationtypesId).HasColumnName("NOTIFICATIONTYPES_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId).IsClustered(false);

            entity.ToTable("ROLES");

            entity.Property(e => e.RolesId).HasColumnName("ROLES_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).IsClustered(false);

            entity.ToTable("USERS");

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.Classid)
                .HasMaxLength(12)
                .HasColumnName("CLASSID");
            entity.Property(e => e.Classname)
                .HasMaxLength(255)
                .HasColumnName("CLASSNAME");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("FULL_NAME");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("PHONE_NUMBER");
            entity.Property(e => e.RolesId).HasColumnName("ROLES_ID");
            entity.Property(e => e.StudentId)
                .HasMaxLength(20)
                .HasColumnName("STUDENT_ID");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");

            entity.HasOne(d => d.Roles).WithMany(p => p.Users)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USERS_USERS_ROL_ROLES");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => new { e.NotificationsId, e.UserId });

            entity.ToTable("USER_NOTIFICATIONS");

            entity.HasIndex(e => e.UserId, "USER_NOTIFICATIONS2_FK");

            entity.HasIndex(e => e.NotificationsId, "USER_NOTIFICATIONS_FK");

            entity.Property(e => e.NotificationsId).HasColumnName("NOTIFICATIONS_ID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_AT");
            entity.Property(e => e.IsDelete).HasColumnName("IS_DELETE");
            entity.Property(e => e.Status)
                .HasMaxLength(60)
                .HasColumnName("STATUS");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_AT");

            entity.HasOne(d => d.Notifications).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.NotificationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_NOT_USER_NOTI_NOTIFICA");

            entity.HasOne(d => d.User).WithMany(p => p.UserNotifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USER_NOT_USER_NOTI_USERS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
