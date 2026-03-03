namespace MiniSocial.DTOs.Profile;

public class ProfileResponseDto(string userName, string? bio, DateTime createdAt, DateTime? updatedAt)
{
    public string Username { get; set; } = userName;
    public string? Bio { get; set; } = bio;
    public DateTime CreatedAt { get; set; } = createdAt;
    public DateTime? UpdatedAt { get; set; } = updatedAt;
}