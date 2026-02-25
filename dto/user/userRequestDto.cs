namespace MiniSocial.Dto;

public class UserRequestDto
{
    public string userName { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string? bio { get; set; } = string.Empty;
}