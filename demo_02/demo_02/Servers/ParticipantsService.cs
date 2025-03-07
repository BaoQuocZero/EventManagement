using demo_02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class ParticipantsService
{
    private readonly EventManagementContext _context;

    public ParticipantsService(EventManagementContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Thêm người tham gia vào sự kiện
    /// </summary>
    public async Task AddParticipantAsync(int eventId, int userId, string status, int earnedPoints, DateTime participationDate)
    {
        var participant = new Eventparticipation
        {
            EventsId = eventId,
            UserId = userId,
            ParticipationStatus = status,
            EarnedPoints = earnedPoints,
            ParticipationTime = participationDate, // Sử dụng ngày được nhập
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            IsDelete = false
        };

        _context.Eventparticipations.Add(participant);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Thêm khoản đóng góp cho sự kiện
    /// </summary>
    public async Task AddDonationAsync(int participationId, int amount, DateTime donationDate)
    {
        var donation = new Eventdonation
        {
            ParticipationId = participationId,
            Amount = amount,
            DonationDate = donationDate, // Sử dụng ngày được nhập
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            IsDelete = false
        };

        _context.Eventdonations.Add(donation);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Kiểm tra xem người dùng đã tham gia sự kiện chưa
    /// </summary>
    public async Task<bool> IsUserParticipatingAsync(int eventId, int userId)
    {
        return await _context.Eventparticipations
            .AnyAsync(ep => ep.EventsId == eventId && ep.UserId == userId && ep.IsDelete == false);
    }
}
