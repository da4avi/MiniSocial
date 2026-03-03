namespace MiniSocial.Dto.Auth;

public class LoginResponseDto(bool succeeded, string? message)
{
    public bool Succeeded { get; set; } = succeeded;
    public string? Message { get; set; } = message;
}