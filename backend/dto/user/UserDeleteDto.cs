namespace MiniSocial.Dto;

public class UserDeleteDto(Guid id)
{
    public Guid Id { get; set; } = id;
}