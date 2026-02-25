using Microsoft.AspNetCore.Mvc;
using MiniSocial.Dto;
using MiniSocial.Models;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/User")]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostUser(UserEntryDto user)
    {
        var result = await _userService.PostUser(user);
        return Ok(result);
    }
}