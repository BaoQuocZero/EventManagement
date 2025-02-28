using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using demo_02.Models;

public class LoginModel : PageModel
{
    private readonly EventManagementContext _context;

    public LoginModel(EventManagementContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnPostAsync([FromBody] LoginInputModel input)
    {
        var user = _context.Users.FirstOrDefault(u =>
            (u.Email == input.LoginInfo || u.PhoneNumber == input.LoginInfo) && (u.IsDelete == false) 
        );

        if (user == null)
        {
            return new JsonResult(new { success = false });
        }

        return new JsonResult(new { success = true });
    }
}

public class LoginInputModel
{
    public string LoginInfo { get; set; }
    public string Password { get; set; }
}
