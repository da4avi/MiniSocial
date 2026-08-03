using MassTransit;

namespace MiniSocial.Configurations;

public static class MassTransitConfig
{
    public static IServiceCollection AddMassTransitConfig(
        this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });  
        });

        return services;
    }
}