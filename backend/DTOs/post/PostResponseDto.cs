namespace MiniSocial.DTOs.Post;

public class PostResponseDto(Guid? id, string? title, string text, Guid userId, DateTime? createdAt, DateTime? updatedAt)
{
    public Guid? Id { get; set; } = id;
    public string? Title { get; set; } = title;
    public string Text { get; set; } = text;
    public Guid UserId { get; set; } = userId;
    public DateTime? CreatedAt { get; set; } = createdAt;
    public DateTime? UpdatedAt { get; set; } = updatedAt;
}