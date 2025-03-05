using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class ParticipantsModel : PageModel
{
    private readonly EventManagementContext _context;

    public ParticipantsModel(EventManagementContext context)
    {
        _context = context;
    }

    public int EventId { get; set; }
    public string EventName { get; set; }
    public List<ParticipantDto> Participants { get; set; }
    public int TotalParticipants { get; set; }
    public int TotalDonations { get; set; }

    public async Task<IActionResult> OnGetAsync(int eventId)
    {
        var eventInfo = await _context.Events
            .Where(e => e.EventsId == eventId)
            .Select(e => new { e.EventsId, e.EventName })
            .FirstOrDefaultAsync();

        if (eventInfo == null)
        {
            return NotFound();
        }

        EventId = eventInfo.EventsId;
        EventName = eventInfo.EventName;

        Participants = await _context.Eventparticipations
            .Where(p => p.EventsId == eventId && p.IsDelete == false)
            .Join(_context.Users,
                p => p.UserId,
                u => u.UserId,
                (p, u) => new ParticipantDto
                {
                    UserId = u.UserId,
                    UserName = u.FullName,
                    Status = p.ParticipationStatus,
                    EarnedPoints = p.EarnedPoints,
                    ParticipationTime = p.ParticipationTime,
                    DonationAmount = _context.Eventdonations
                        .Where(d => d.ParticipationId == p.ParticipationId)
                        .Sum(d => (int?)d.Amount) ?? 0
                })
            .ToListAsync();

        TotalParticipants = Participants.Count;
        TotalDonations = Participants.Sum(p => p.DonationAmount);

        return Page();
    }

    public class ParticipantDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public int? EarnedPoints { get; set; }
        public DateTime? ParticipationTime { get; set; }
        public int DonationAmount { get; set; }
    }
}
