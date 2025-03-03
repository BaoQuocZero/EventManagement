using Bogus;
using demo_02.Models;
using Microsoft.EntityFrameworkCore;

public class DatabaseSeeder
{
    private readonly EventManagementContext _context;

    public DatabaseSeeder(EventManagementContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        SeedUsers();
        SeedEvents();
        SeedEventParticipations();
        SeedEventDonations();
        _context.SaveChanges();
    }

    private void SeedUsers()
    {
        var roleIds = _context.Roles.Select(r => r.RolesId).ToList();

        var usersFaker = new Faker<User>()
            .RuleFor(u => u.RolesId, f => f.PickRandom(roleIds))
            .RuleFor(u => u.StudentId, f => f.Random.Replace("S########"))
            .RuleFor(u => u.FullName, f => f.Name.FullName())
            .RuleFor(u => u.Classid, f => f.Random.AlphaNumeric(6))
            .RuleFor(u => u.Classname, f => f.Random.Word())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("0#########"))
            .RuleFor(u => u.Password, f => f.Internet.Password(10))
            .RuleFor(u => u.CreateAt, f => f.Date.Past(2))
            .RuleFor(u => u.UpdateAt, f => f.Date.Recent())
            .RuleFor(u => u.IsDelete, f => f.Random.Bool(0.1f));

        var users = usersFaker.Generate(100); // Thêm 100 user mỗi lần chạy
        _context.Users.AddRange(users);
    }

    private void SeedEvents()
    {
        var eventTypeIds = _context.Eventtypes.Select(et => et.EventtypesId).ToList();

        var eventsFaker = new Faker<Event>()
            .RuleFor(e => e.EventtypesId, f => f.PickRandom(eventTypeIds))
            .RuleFor(e => e.EventName, f => f.Company.CatchPhrase())
            .RuleFor(e => e.EventTime, f => f.Date.Future())
            .RuleFor(e => e.EndTime, (f, e) => e.EventTime.HasValue ? e.EventTime.Value.AddHours(f.Random.Int(2, 5)) : DateTime.Now)
            .RuleFor(e => e.Location, f => f.Address.City())
            .RuleFor(e => e.Description, f => f.Lorem.Paragraph())
            .RuleFor(e => e.RequiredParticipants, f => f.Random.Int(10, 100))
            .RuleFor(e => e.MaxParticipants, (f, e) => e.RequiredParticipants + f.Random.Int(10, 50))
            .RuleFor(e => e.CreateAt, f => f.Date.Past(2))
            .RuleFor(e => e.UpdateAt, f => f.Date.Recent())
            .RuleFor(e => e.IsDelete, f => f.Random.Bool(0.1f));

        var events = eventsFaker.Generate(50); // Thêm 50 sự kiện mỗi lần chạy
        _context.Events.AddRange(events);
    }

    private void SeedEventParticipations()
    {
        var userIds = _context.Users.Select(u => u.UserId).ToList();
        var eventIds = _context.Events.Select(e => e.EventsId).ToList();

        var participationFaker = new Faker<Eventparticipation>()
            .RuleFor(p => p.EventsId, f => f.PickRandom(eventIds))
            .RuleFor(p => p.UserId, f => f.PickRandom(userIds))
            .RuleFor(p => p.ParticipationStatus, f => f.PickRandom(new[] { "Đã đăng ký", "Đã tham dự", "Vắng" }))
            .RuleFor(p => p.EarnedPoints, f => f.Random.Int(5, 50))
            .RuleFor(p => p.ParticipationTime, f => f.Date.Past(1))
            .RuleFor(p => p.CreateAt, f => f.Date.Past(1))
            .RuleFor(p => p.UpdateAt, f => f.Date.Recent())
            .RuleFor(p => p.IsDelete, f => f.Random.Bool(0.05f));

        var participations = participationFaker.Generate(150); // Thêm 150 lượt tham gia mỗi lần chạy
        _context.Eventparticipations.AddRange(participations);
    }

    private void SeedEventDonations()
    {
        var participationIds = _context.Eventparticipations.Select(p => p.ParticipationId).ToList();

        var donationFaker = new Faker<Eventdonation>()
            .RuleFor(d => d.ParticipationId, f => f.PickRandom(participationIds))
            .RuleFor(d => d.Amount, f => f.Random.Int(10, 500))
            .RuleFor(d => d.DonationDate, f => f.Date.Past(1))
            .RuleFor(d => d.CreateAt, f => f.Date.Past(1))
            .RuleFor(d => d.UpdateAt, f => f.Date.Recent())
            .RuleFor(d => d.IsDelete, f => f.Random.Bool(0.05f));

        var donations = donationFaker.Generate(100); // Thêm 100 lượt quyên góp mỗi lần chạy
        _context.Eventdonations.AddRange(donations);
    }
}
