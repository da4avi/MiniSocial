namespace MiniSocial.Dto;

public class LoginResponseDto(Guid? id, string userName)
{
    public Guid? Id { get; set; } = id;
    public string UserName { get; set; } = userName;
}