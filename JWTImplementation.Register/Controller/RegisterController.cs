using JWTImplementation.Models.DTO.Request;
using JWTImplementation.Models.DTO.Response;
using JWTImplementation.Register.Auth;
using Microsoft.AspNetCore.Mvc;

namespace JWTImplementation.Register.Controller;

[ApiController]
[Route("api/[controller]")]
public class RegisterController(
    IAuthService authService
) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest user)
    {
        var token = _authService.GenerateToken(user);

        return Ok(new TokenResponse
        {
            AccessToken = token.AccessToken,
            ExpiresAt = DateTime.Now.AddHours(24)
        });
    }
}
