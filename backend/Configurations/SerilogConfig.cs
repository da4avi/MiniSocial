using Serilog;

namespace MiniSocial.Configurations;

public static class SerilogConfig
{
    public static void AddSerilogConfig(
        this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger(); 

        builder.Host.UseSerilog();
    }
}