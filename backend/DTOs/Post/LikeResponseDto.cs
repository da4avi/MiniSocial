namespace MiniSocial.DTOs.Post;

public class LikeResponseDto(Guid postId, int likes)
{
    public Guid PostId { get; set; } = postId;
    public int Likes { get; set; } = likes;
}