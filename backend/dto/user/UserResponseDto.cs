namespace MiniSocial.Dto.User;

public class UserResponseDto(string id, string? userName)
{
    public string Id { get; set; } = id;
    public string? UserName { get; set; } = userName;
}