using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Post;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class PostService(AppDbContext context)
{
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

    public async Task<PostResponseDto> PostPost(PostRequestDto postRequest)
    {
        //busca se o id do usuario existe
        // if (!await _context.Users.AnyAsync(user => user.Id == postRequest.UserId)) throw new Exception("Id not found");

        //cria o post
        Post post = new(postRequest.Title, postRequest.Text, postRequest.UserId);

        //adiciona no banco
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return new PostResponseDto(post.Id, post.Title, post.Text, post.UserId, post.CreatedAt, post.UpdatedAt);
    }
}