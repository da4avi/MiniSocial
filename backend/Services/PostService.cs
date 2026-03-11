using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Post;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class PostService(AppDbContext context, UserManager<IdentityUser> userManager, ProfileService profileService)
{
    private readonly UserManager<IdentityUser> _userManager = userManager;

    private readonly AppDbContext _context = context;

    private readonly ProfileService _profileService = profileService;

    public async Task<List<PostResponseDto>> GetPosts()
    {
        //pega todos os posts e cria uma lista mapeando pra PostResponseDto
        return await _context.Posts.AsNoTracking().Select(post => new PostResponseDto(
            post.Id,
            post.Title,
            post.Text,
            post.Likes,
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
            post.Likes,
            post.UserId,
            post.CreatedAt,
            post.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task<LikeResponseDto> LikePost(LikeRequestDto likeRequest, string userId)
    {
        //procura o post
        Post post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == likeRequest.PostId) ?? throw new KeyNotFoundException($"Post with ID {likeRequest.PostId} not Found");
        post.Likes ++;

        //procura o profile
        Profile profile = await _context.Profiles.FirstOrDefaultAsync(profile => profile.IdentityId == userId) ?? throw new KeyNotFoundException($"Profile with ID {userId} not Found");
        profile.LikedPosts.Add(likeRequest.PostId);

        await _context.SaveChangesAsync();

        return new LikeResponseDto(post.Id, post.Likes);
    }

    public async Task<PostResponseDto> PostPost(PostRequestDto postRequest, string userId)
    {
        //cria o post
        Post post = new(postRequest.Title, postRequest.Text, 0, userId);

        //adiciona no banco
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return new PostResponseDto(post.Id, post.Title, post.Text, post.Likes, post.UserId, post.CreatedAt, post.UpdatedAt);
    }
}