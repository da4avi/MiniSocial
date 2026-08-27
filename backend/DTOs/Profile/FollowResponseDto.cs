namespace MiniSocial.DTOs.Profile;

public class FollowResponseDto(bool succeeded)
{
    public bool Succeeded { get; set; } = succeeded;
}