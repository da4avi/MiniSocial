using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> PostUser(User user)
    {
        var result = await _userService.PostUser(user);
        return Ok(result);
    }
}