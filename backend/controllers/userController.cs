using Microsoft.AspNetCore.Mvc;
using MiniSocial.Dto;
using MiniSocial.Models;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();
        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequestDto login)
    {
        var result = await _userService.Login(login);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser(UserRequestDto userRequest)
    {
        var result = await _userService.PostUser(userRequest);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdate)
    {
        var result = await _userService.UpdateUser(userUpdate);
        return Ok(result);
    }
}