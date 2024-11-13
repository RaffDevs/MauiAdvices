using MauiAdvices.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MauiAdvices.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<HttpClient>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AdviceService>();
        
        return services;
    }
}