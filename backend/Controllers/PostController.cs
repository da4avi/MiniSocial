using Microsoft.AspNetCore.Mvc;
using MiniSocial.DTOs.Post;
using MiniSocial.Services;

namespace MiniSocial.Controllers;

[ApiController]
[Route("api/Post")]
public class PostController(PostService postService) : ControllerBase
{
    private readonly PostService _postService = postService;

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var result = await _postService.GetPosts();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostPost(PostRequestDto postRequest)
    {
        var result = await _postService.PostPost(postRequest);
        return Ok(result);
    }
}