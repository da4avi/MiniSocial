namespace MiniSocial.Dto.User;

public class UserResponseDto(Guid? id, string userName, DateTime? createdAt, DateTime? updatedAt)
{
    public Guid? Id { get; set; } = id;
    public string UserName { get; set; } = userName;
    public DateTime? CreatedAt { get; set; } = createdAt;
    public DateTime? UpdatedAt { get; set; } = updatedAt;
}