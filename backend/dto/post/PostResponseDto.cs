namespace MiniSocial.Dto.Post;

public class PostResponseDto(Guid? id, string? title, string text, Guid userId)
{
    public Guid? Id { get; set; } = id;
    public string? Title { get; set; } = title;
    public string Text { get; set; } = text;
    public Guid UserId { get; set; } = userId;
}