using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Post;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class PostService(AppDbContext context, UserManager<IdentityUser> userManager)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private readonly AppDbContext _context = context;

    public async Task<List<PostResponseDto>> GetPosts()
    {
        //pega todos os posts e cria uma lista mapeando pra PostResponseDto
        return await _context.Posts.AsNoTracking().Select(post => new PostResponseDto(
            post.Id,
            post.Title,
            post.Text,
            post.UserId,
            post.CreatedAt,
            post.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task<List<PostResponseDto>> GetMyPosts(string userId)
    {
        //pega os posts do usuario logado e cria uma lista mapeando pra PostResponseDto
        return await _context.Posts.AsNoTracking()
        .Where(post => post.UserId == userId)
        .Select(post => new PostResponseDto(
            post.Id,
            post.Title,
            post.Text,
            post.UserId,
            post.CreatedAt,
            post.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task<PostResponseDto> PostPost(PostRequestDto postRequest, string userId)
    {
        //cria o post
        Post post = new(postRequest.Title, postRequest.Text, userId);

        //adiciona no banco
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return new PostResponseDto(post.Id, post.Title, post.Text, post.UserId, post.CreatedAt, post.UpdatedAt);
    }
}