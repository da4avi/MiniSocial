namespace MiniSocial.DTOs.Profile;

public class ProfileResponseDto(string userName, string? bio, List<Guid> likedPosts, DateTime createdAt, DateTime updatedAt)
{
    public string Username { get; set; } = userName;
    public string? Bio { get; set; } = bio;
    public List<Guid> LikedPosts { get; set; } = likedPosts;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime UpdatedAt { get; set; } = updatedAt;
}