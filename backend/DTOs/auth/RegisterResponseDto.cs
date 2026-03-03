namespace MiniSocial.DTOs.Auth;

public class RegisterResponseDto(bool succeeded, IEnumerable<string>? message)
{
    public bool Succeeded { get; set; } = succeeded;
    public IEnumerable<string>? Errors { get; set; } = message;
}