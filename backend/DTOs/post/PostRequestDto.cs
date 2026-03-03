namespace MiniSocial.DTOs.Post;

public class PostRequestDto(string? title, string text, Guid userId)
{
    public string? Title { get; set; } = title;
    public string Text { get; set; } = text;
    public Guid UserId { get; set; } = userId;
}