using Microsoft.AspNetCore.Mvc;
using MiniSocial.DTOs.Post;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/Profile")]
public class ProfileController(ProfileService profileService) : ControllerBase
{
    private readonly ProfileService _profileService = profileService;

    [HttpGet]
    public async Task<IActionResult> GetAllProfiles()
    {
        var result = await _profileService.GetAllProfiles();
        return Ok(result);
    }
}