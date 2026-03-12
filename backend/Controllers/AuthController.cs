using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSocial.DTOs.Auth;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
    {
        var result = await _authService.Register(registerRequest);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequest)
    {
        var result = await _authService.Login(loginRequest);
        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.Logout();
        return NoContent();
    }

    [HttpPut("updateUser")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UserUpdateRequestDto userUpdateRequest)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _authService.UpdateUser(userUpdateRequest, userId);
        return Ok(result);
    }
}