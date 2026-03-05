using Microsoft.EntityFrameworkCore;
using MiniSocial.Data;

namespace MiniSocial.Configurations;

public static class DbConfig
{
    public static IServiceCollection AddDbConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")
        ));

        return services;
    }
}