using MiniSocial.DTOs.Exception;

namespace MiniSocial.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    //define qual o prox middleware
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    //chamado toda vez que acontece alguma requisiçao http
    public async Task InvokeAsync(HttpContext context)
    {
        DateTime start = DateTime.UtcNow;
        try
        {
            //executa o resto da pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            TimeSpan duration = DateTime.UtcNow - start;
            //log de erro
            _logger.LogError("\nMethod: {Method} \nPath: {Path} \nMessage: {Message} \nDuration: {Duration}ms", context.Request.Method, context.Request.Path, ex.Message, duration.TotalMilliseconds);

            //monta a response da exception
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            ExceptionResponseDto exception = new(StatusCodes.Status500InternalServerError, "Server Error");

            await context.Response.WriteAsJsonAsync(exception);
        }
    }
}