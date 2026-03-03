namespace MiniSocial.Models;

public class Profile(string userName, string? bio, string identityId)
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string UserName { get; set; } = userName;
    public string? Bio { get; set; } = bio;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string IdentityId { get; set; } = identityId;
}