using MiniSocial.DTOs.Auth;
using MiniSocial.DTOs.Profile;

namespace MiniSocial.Services.Interfaces;

public interface IProfileService
{
    public Task<List<ProfileResponseDto>> GetAllProfiles();
    public Task<ProfileResponseDto> GetMyProfile(string userId);
    public Task RegisterProfile(RegisterRequestDto registerRequest, string id);
    public Task UpdateProfile(UserUpdateRequestDto userUpdateRequest, string id);
}