using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;
using MiniSocial.DTOs.Auth;
using MiniSocial.DTOs.Profile;
using MiniSocial.Models;

namespace MiniSocial.Services;

public class ProfileService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<List<ProfileResponseDto>> GetAllProfiles()
    {
        return await _context.Profiles.AsNoTracking().Select(userProfile => new ProfileResponseDto(
            userProfile.UserName,
            userProfile.Bio,
            userProfile.CreatedAt,
            userProfile.UpdatedAt
        ))
        .ToListAsync();
    }

    public async Task RegisterProfile(RegisterRequestDto registerRequest, string id)
    {
        Profile profile = new(registerRequest.UserName, registerRequest.Bio, id);

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }
}