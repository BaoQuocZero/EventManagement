using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Linq;
using demo_02.Models;
using BCrypt.Net;

public class LoginModel : PageModel
{
    private readonly EventManagementContext _context;

    // Inject EventManagementContext vào constructor
    public LoginModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LoginInputModel Input { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(Input.LoginInfo) || string.IsNullOrEmpty(Input.Password))
        {
            ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
            return Page();
        }

        // Kiểm tra người dùng trong database
        var user = _context.Users.FirstOrDefault(u =>
            (u.Email == Input.LoginInfo || u.PhoneNumber == Input.LoginInfo) && (u.IsDelete == false)
        );

        if (user == null || !BCrypt.Net.BCrypt.Verify(Input.Password, user.Password))
        {
            ErrorMessage = "Thông tin đăng nhập không chính xác.";
            return Page();
        }

        // Lưu thông tin user vào session
        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("FullName", user.FullName);
        HttpContext.Session.SetString("Email", user.Email);
        HttpContext.Session.SetString("RolesId", user.RolesId.ToString());

        // Kiểm tra xem session đã được lưu chưa
        var checkUserId = HttpContext.Session.GetString("UserId");
        var checkFullName = HttpContext.Session.GetString("FullName");
        Console.WriteLine($"UserId trong session: {checkUserId}");
        Console.WriteLine($"FullName trong session: {checkFullName}");

        // Chuyển hướng sau khi đăng nhập thành công
        return RedirectToPage("/Index");
    }
}

public class LoginInputModel
{
    public string LoginInfo { get; set; }
    public string Password { get; set; }
}