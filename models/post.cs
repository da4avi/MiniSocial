namespace MiniSocial.Models;

public class Post
{
    public int? Id { get; set; }
    public string? title { get; set; } = string.Empty;
    public string text { get; set; } = string.Empty;
    public int userId { get; set; }
}