using MiniSocial.Data;
using MiniSocial.Dto.Post;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class PostService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<PostResponseDto> PostPost(PostRequestDto postRequest)
    {
        Post post = new(postRequest.Title, postRequest.Text, postRequest.UserId);

        // _posts.Add(post);
        // await Task.Delay(100);

        return new PostResponseDto(post.Id, post.Title, post.Text, post.UserId);
    }
}