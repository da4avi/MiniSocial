namespace MiniSocial.Models;

public class Post(string? title, string text, Guid userId)
{
    public Guid? Id { get; set; } = Guid.CreateVersion7();
    public string? Title { get; set; } = title;
    public string Text { get; set; } = text;
    public Guid UserId { get; set; } = userId;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}