namespace MiniSocial.DTOs.Notifications;

public class LikeEventDto(string postUserId, Guid likeUserId, Guid postId)
{
    public string PostUserId { get; init; } = postUserId;
    public Guid LikerUserId { get; init; } = likeUserId;
    public Guid PostId { get; init; } = postId;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}