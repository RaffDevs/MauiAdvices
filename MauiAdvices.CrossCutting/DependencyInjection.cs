using MauiAdvices.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MauiAdvices.CrossCutting;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<HttpClient>();
        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AdviceService>();
        services.AddScoped<TranslatorService>();
        
        return services;
    }
}