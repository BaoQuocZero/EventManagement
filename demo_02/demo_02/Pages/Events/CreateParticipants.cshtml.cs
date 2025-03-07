using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CreateParticipantsModel : PageModel
{
    private readonly ParticipantsService _participantsService;
    private readonly EventManagementContext _context;

    public CreateParticipantsModel(ParticipantsService participantsService, EventManagementContext context)
    {
        _participantsService = participantsService;
        _context = context;
    }

    public int EventId { get; set; }
    public List<User> Users { get; set; }

    [BindProperty]
    public ParticipantRequest Input { get; set; } // Binding dữ liệu từ form

    public async Task<IActionResult> OnGetAsync(int id)
    {
        EventId = id;
        Users = await _context.Users
            .Where(u => (bool)!u.IsDelete)
            .Take(50)
            .ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!ModelState.IsValid || Input == null || Input.UserId == 0 || string.IsNullOrEmpty(Input.Status) || Input.ParticipationDate == null)
        {
            Users = await _context.Users
                .Where(u => (bool)!u.IsDelete)
                .Take(50)
                .ToListAsync();
            EventId = Input.EventId;
            return Page(); // Trả lại trang nếu dữ liệu không hợp lệ
        }

        await _participantsService.AddParticipantAsync(
            Input.EventId,
            Input.UserId,
            Input.Status,
            Input.EarnedPoints,
            Input.ParticipationDate
        );

        var participant = await _context.Eventparticipations
            .FirstOrDefaultAsync(p => p.EventsId == Input.EventId && p.UserId == Input.UserId);

        if (participant != null && Input.DonationAmount > 0)
        {
            await _participantsService.AddDonationAsync(
                participant.ParticipationId,
                Input.DonationAmount,
                Input.DonationDate ?? DateTime.Now
            );
        }

        TempData["SuccessMessage"] = "Thêm người tham gia thành công!";
        return RedirectToPage("Index");
    }
}

public class ParticipantRequest
{
    public int EventId { get; set; }
    public int UserId { get; set; }
    public int DonationAmount { get; set; }
    public DateTime? DonationDate { get; set; }
    public string Status { get; set; }
    public int EarnedPoints { get; set; }
    public DateTime ParticipationDate { get; set; }
    public string Notes { get; set; }
}