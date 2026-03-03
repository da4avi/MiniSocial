using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController(ProfileService profileService) : ControllerBase
{
    private readonly ProfileService _profileService = profileService;

    [HttpGet]
    public async Task<IActionResult> GetAllProfiles()
    {
        var result = await _profileService.GetAllProfiles();
        return Ok(result);
    }

    [HttpGet("myProfile")]
    [Authorize]
    public async Task<IActionResult> GetMyProfile()
    {
        //pega o valor da claim que tem o id do usuario logado
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _profileService.GetMyProfile(userId);
        return Ok(result);
    }
}