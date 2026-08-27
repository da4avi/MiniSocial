using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSocial.DTOs.Profile;
using MiniSocial.Services.Interfaces;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController(IProfileService profileService) : ControllerBase
{
    private readonly IProfileService _profileService = profileService;

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

    [HttpPatch("followProfile")]
    [Authorize]
    public async Task<IActionResult> FollowProfile(FollowRequestDto followRequest)
    {
        //pega o valor da claim que tem o id do usuario logado
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _profileService.FollowProfile(followRequest, userId);
        return Ok(result);
    }
}