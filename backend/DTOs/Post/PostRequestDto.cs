namespace MiniSocial.DTOs.Post;

public class PostRequestDto
{
    public string? Title { get; set; }
    public required string Text { get; set; }
}