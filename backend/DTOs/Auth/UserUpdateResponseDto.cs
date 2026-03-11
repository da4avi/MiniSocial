namespace MiniSocial.DTOs.Auth;

public class UserUpdateResponseDto(bool succeeded, IEnumerable<string>? message)
{
    public bool Succeeded { get; set; } = succeeded;
    public IEnumerable<string>? Errors { get; set; } = message;
}