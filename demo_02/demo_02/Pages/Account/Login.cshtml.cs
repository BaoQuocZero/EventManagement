using demo_02.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BCrypt.Net;

public class LoginModel : PageModel
{
    private readonly EventManagementContext _context;

    public LoginModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public LoginInputModel Input { get; set; }

    public string ErrorMessage { get; set; }
    public string LoggedInUserId { get; set; }
    public string LoggedInFullName { get; set; }
    public string LoggedInEmail { get; set; }
    public string LoggedInRolesId { get; set; }

    public IActionResult OnPost()
    {
        if (string.IsNullOrEmpty(Input.LoginInfo) || string.IsNullOrEmpty(Input.Password))
        {
            ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
            return Page();
        }
        var user = _context.Users.FirstOrDefault(u =>
            (u.Email == Input.LoginInfo || u.PhoneNumber == Input.LoginInfo) && (u.IsDelete == false)
        );

        if (user == null || !BCrypt.Net.BCrypt.Verify(Input.Password, user.Password))
        {
            ErrorMessage = "Thông tin đăng nhập không chính xác.";
            return Page();
        }

        // Lưu thông tin vào session
        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("FullName", user.FullName);
        HttpContext.Session.SetString("Email", user.Email);
        HttpContext.Session.SetString("RolesId", user.RolesId.ToString());

        // Gán giá trị để hiển thị trên giao diện
        LoggedInUserId = user.UserId.ToString();
        LoggedInFullName = user.FullName;
        LoggedInEmail = user.Email;
        LoggedInRolesId = user.RolesId.ToString();

        return Page(); // Quay lại chính trang đăng nhập nhưng hiển thị thông tin user
    }
}

public class LoginInputModel
{
    public string LoginInfo { get; set; }
    public string Password { get; set; }
}
