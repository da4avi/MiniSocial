using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSocial.DTOs.Post;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/post")]
public class PostController(PostService postService) : ControllerBase
{
    private readonly PostService _postService = postService;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostPost(PostRequestDto postRequest)
    {
        //pega o valor da claim que tem o id do usuario logado
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _postService.PostPost(postRequest, userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var result = await _postService.GetPosts();
        return Ok(result);
    }

    [HttpGet("myPosts")]
    [Authorize]
    public async Task<IActionResult> GetMyPosts()
    {
        //pega o valor da claim que tem o id do usuario logado
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var result = await _postService.GetMyPosts(userId);
        return Ok(result);
    }
}