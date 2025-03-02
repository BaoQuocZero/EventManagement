using demo_02.Models;
using Microsoft.EntityFrameworkCore;

public class AuthService
{
    private readonly EventManagementContext _context;

    public AuthService(EventManagementContext context)
    {
        _context = context;
    }

    public async Task<User?> AuthenticateUser(string email, string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>
            u.Email == email && u.PhoneNumber == phoneNumber && u.IsDelete == false);
    }
}
