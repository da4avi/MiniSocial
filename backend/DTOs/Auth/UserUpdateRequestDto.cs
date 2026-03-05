namespace MiniSocial.DTOs.Auth;

public class UserUpdateRequestDto
{
    //userIdentity
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }

    //userProfile
    public string? Bio { get; set;}
}