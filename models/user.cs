namespace MiniSocial.Models;

public class User
{
    public int Id { get; set; }
    public string userName { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string? bio { get; set; } = string.Empty;
}