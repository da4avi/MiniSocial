namespace MiniSocial.DTOs.Auth;

public class RegisterRequestDto()
{
    //userIdentity
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

     //userProfile
    public string? Bio { get; set;}
}