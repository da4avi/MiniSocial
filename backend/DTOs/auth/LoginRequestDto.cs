namespace MiniSocial.DTOs.Auth;

public class LoginRequestDto
{
    //userIdentity
    public required string UserName { get; set; }
    public required string Password { get; set; }
}