namespace MiniSocial.DTOs.Exception;

public class ExceptionResponseDto(int status, string message)
{
    public int Status { get; set; } = status;
    public string Message { get; set; } = message;
}