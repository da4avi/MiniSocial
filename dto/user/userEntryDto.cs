namespace MiniSocial.Dto;

public class UserEntryDto
{
    public string userName { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string? bio { get; set; } = string.Empty;
}