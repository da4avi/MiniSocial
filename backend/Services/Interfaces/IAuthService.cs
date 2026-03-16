using MiniSocial.DTOs.Auth;

namespace MiniSocial.Services.Interfaces;

public interface IAuthService
{
    public Task<LoginResponseDto> Login(LoginRequestDto loginRequest);
    public Task Logout();
    public Task<RegisterResponseDto> Register(RegisterRequestDto registerRequest);
    public Task<UserUpdateResponseDto> UpdateUser(UserUpdateRequestDto userUpdateRequest, string userId);
}