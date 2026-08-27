using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Auth;
using MiniSocial.DTOs.Profile;
using MiniSocial.Models;
using MiniSocial.Services.Interfaces;

namespace MiniSocial.Services;

public class ProfileService(AppDbContext context) : IProfileService
{
    private readonly AppDbContext _context = context;

    public async Task<List<ProfileResponseDto>> GetAllProfiles()
    {
        //pega todos os profiles e mapeia pro dto
        return await _context.Profiles.AsNoTracking().Select(userProfile => new ProfileResponseDto(
            userProfile.UserName,
            userProfile.Bio,
            userProfile.LikedPosts,
            userProfile.CreatedAt,
            userProfile.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task<ProfileResponseDto> GetMyProfile(string userId)
    {
        //pega o usuario logado e mapeia pro dto
        return await _context.Profiles.AsNoTracking()
        .Where(user => user.IdentityId == userId)
        .Select(userProfile => new ProfileResponseDto(
            userProfile.UserName,
            userProfile.Bio,
            userProfile.LikedPosts,
            userProfile.CreatedAt,
            userProfile.UpdatedAt
        ))
        .SingleAsync();
    }

    public async Task RegisterProfile(RegisterRequestDto registerRequest, string id)
    {
        //cria um profile usando o que vem do registro
        Profile profile = new(registerRequest.UserName, registerRequest.Bio, [], [], [], id);

        //adiciona no banco
        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProfile(UserUpdateRequestDto userUpdateRequest, string id)
    {
        //procura seu perfil
        Profile profile = await _context.Profiles.FirstOrDefaultAsync(profile => profile.IdentityId == id) ?? throw new KeyNotFoundException($"Profile with ID {id} not Found");

        //ve o que tem pra atualizar
        if (!string.IsNullOrWhiteSpace(userUpdateRequest.UserName)) profile.UserName = userUpdateRequest.UserName;
        if (!string.IsNullOrWhiteSpace(userUpdateRequest.Bio)) profile.Bio = userUpdateRequest.Bio;

        profile.UpdatedAt = DateTime.UtcNow;

        //adiciona no banco
        await _context.SaveChangesAsync();
    }

    public async Task<FollowResponseDto> FollowProfile(FollowRequestDto followRequestDto, string id)
    {
        //perfis
        Profile followed = await _context.Profiles.FirstOrDefaultAsync(profile => profile.IdentityId == followRequestDto.UserId) ?? throw new KeyNotFoundException($"Profile with ID {followRequestDto.UserId} not Found");
        Profile follower = await _context.Profiles.FirstOrDefaultAsync(profile => profile.IdentityId == id) ?? throw new KeyNotFoundException($"Profile with ID {id} not Found");

        if (followRequestDto.UserId == id) throw new InvalidOperationException("Follower and Followed ID's are the same");

        //toggle de follow
        if (!string.IsNullOrWhiteSpace(id) && follower.Following.Contains(followed.IdentityId))
        {
            follower.Following.Remove(followed.IdentityId);
            followed.Followers.Remove(follower.IdentityId);
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(followRequestDto.UserId)) followed.Followers.Add(id);
            if (!string.IsNullOrWhiteSpace(id)) follower.Following.Add(followRequestDto.UserId);
        }

        //atualiza no banco
        await _context.SaveChangesAsync();

        return new FollowResponseDto(true);
    }
}