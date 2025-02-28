using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.AuthenticateUser(request.Email, request.PhoneNumber);
        if (user == null)
        {
            return Unauthorized(new { message = "Sai email hoặc số điện thoại." });
        }
        return Ok(new { message = "Đăng nhập thành công!" });
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
