namespace MiniSocial.Dto.User;

public class UserDeleteDto(Guid id)
{
    public Guid Id { get; set; } = id;
}