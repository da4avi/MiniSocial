namespace MiniSocial.Dto;

public class UserUpdateDto(Guid id, string? userName, string? password, string? bio)
{
    public Guid Id { get; set; } = id;
    public string? UserName { get; set; } = userName;
    public string? Password { get; set; } = password;
    public string? Bio { get; set; } = bio;
}