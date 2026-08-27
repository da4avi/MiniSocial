namespace MiniSocial.Models;

public class Profile(string userName, string? bio, List<Guid> likedPosts, List<string> followers, List<string> following, string identityId)
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string UserName { get; set; } = userName;
    public string? Bio { get; set; } = bio;
    public List<Guid> LikedPosts { get; set; } = likedPosts;
    public List<string> Followers { get; set; } = followers;
    public List<string> Following { get; set; } = following;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string IdentityId { get; set; } = identityId;
}