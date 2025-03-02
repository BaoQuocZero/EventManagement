using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using demo_02.Models;
using BCrypt.Net;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.CodeAnalysis.Scripting;

public class RegisterModel : PageModel
{
    private readonly EventManagementContext _context;

    public RegisterModel(EventManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public RegisterInputModel Input { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
        // Hiển thị form đăng ký
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (_context.Users.Any(u => u.StudentId == Input.StudentId))
        {
            ErrorMessage = "Mã sinh viên đã tồn tại.";
            return Page();
        }

        if (_context.Users.Any(u => u.Email == Input.Email || u.PhoneNumber == Input.PhoneNumber))
        {
            ErrorMessage = "Email hoặc số điện thoại đã tồn tại.";
            return Page();
        }

        if (Input.Password != Input.ConfirmPassword)
        {
            ErrorMessage = "Mật khẩu nhập lại không khớp.";
            return Page();
        }

        // Mã hóa mật khẩu
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Input.Password);

        var newUser = new User
        {
            StudentId = Input.StudentId,
            FullName = Input.FullName,
            Email = Input.Email,
            PhoneNumber = Input.PhoneNumber,
            Classid = Input.Classid,
            Classname = Input.Classname,
            Password = hashedPassword,
            RolesId = 2, // Mặc định là "User"
            CreateAt = DateTime.Now,
            IsDelete = false,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Account/Login");
    }
}

public class RegisterInputModel
{
    public string StudentId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Classid { get; set; } // Mã lớp
    public string Classname { get; set; } // Tên lớp
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
