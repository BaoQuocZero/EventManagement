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
    public string LoggedInPhone { get; set; }      // ✅ Thêm số điện thoại
    public string LoggedInStudentId { get; set; }  // ✅ Thêm mã sinh viên
    public string LoggedInClassId { get; set; }    // ✅ Thêm mã lớp
    public string LoggedInClassName { get; set; }  // ✅ Thêm tên lớp
    public string LoggedInCreatedAt { get; set; }  // ✅ Thêm ngày tạo tài khoản

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
        HttpContext.Session.SetString("Phone", user.PhoneNumber); // ✅ Lưu số điện thoại
        HttpContext.Session.SetString("StudentId", user.StudentId ?? ""); // ✅ Lưu mã sinh viên
        HttpContext.Session.SetString("ClassId", user.Classid ?? ""); // ✅ Lưu mã lớp
        HttpContext.Session.SetString("ClassName", user.Classname ?? ""); // ✅ Lưu tên lớp
        HttpContext.Session.SetString("CreatedAt", user.CreateAt?.ToString("dd/MM/yyyy") ?? ""); // ✅ Lưu ngày tạo tài khoản

        // Gán giá trị để hiển thị trên giao diện
        LoggedInUserId = user.UserId.ToString();
        LoggedInFullName = user.FullName;
        LoggedInEmail = user.Email;
        LoggedInRolesId = user.RolesId.ToString();
        LoggedInPhone = user.PhoneNumber; // ✅ Hiển thị số điện thoại
        LoggedInStudentId = user.StudentId; // ✅ Hiển thị mã sinh viên
        LoggedInClassId = user.Classid; // ✅ Hiển thị mã lớp
        LoggedInClassName = user.Classname; // ✅ Hiển thị tên lớp
        LoggedInCreatedAt = user.CreateAt?.ToString("dd/MM/yyyy"); // ✅ Hiển thị ngày tạo tài khoản

        return Page(); // Quay lại chính trang đăng nhập nhưng hiển thị thông tin user
    }
}

public class LoginInputModel
{
    public string LoginInfo { get; set; }
    public string Password { get; set; }
}
