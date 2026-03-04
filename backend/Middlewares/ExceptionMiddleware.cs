using MiniSocial.DTOs.Exception;

namespace MiniSocial.Middlewares;

public class ExceptionMiddleware (RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    //define qual o prox middleware
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    //chamado toda vez que acontece alguma requisiçao http
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //executa o resto da pipeline
            await _next(context);
        } 
        catch(Exception ex)
        {
            _logger.LogError("\n" + "log - Exception \n" + "Path: {Path} \n" + "Exception: {Message}", context.Request.Path, ex.Message);
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ExceptionResponseDto exception = new(StatusCodes.Status500InternalServerError, "Server Error");

            await context.Response.WriteAsJsonAsync(exception);
        }
    }
}