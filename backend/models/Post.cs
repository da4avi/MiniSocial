namespace MiniSocial.Models;

public class Post
{
    public Guid? Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public int UserId { get; set; }
}