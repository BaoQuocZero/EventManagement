using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    private readonly AuthService _authService;

    public LoginModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string PhoneNumber { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _authService.AuthenticateUser(Email, PhoneNumber);

        if (user == null)
        {
            return new JsonResult(new { success = false });
        }

        return new JsonResult(new { success = true });
    }
}
