using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MiniSocial.Dto.User;
using MiniSocial.Models;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    private readonly AuthService _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _authService.Login(loginRequest);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var result = await _authService.Register(registerRequest);
        return Ok(result);
    }

    // [HttpPatch]
    // public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdate)
    // {
    //     var result = await _userService.UpdateUser(userUpdate);
    //     return Ok(result);
    // }

    // [HttpDelete]
    // public async Task<IActionResult> DeleteUser(UserDeleteDto userDelete)
    // {
    //     await _userService.DeleteUser(userDelete);
    //     return NoContent();
    // }
}