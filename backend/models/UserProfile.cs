namespace MiniSocial.Models;

public class UserProfile(string userName, string password, string? bio)
{
    public Guid? Id { get; set; } = Guid.CreateVersion7();
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
    public string? Bio { get; set; } = bio;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}