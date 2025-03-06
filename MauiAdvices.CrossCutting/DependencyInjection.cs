using MauiAdvices.Core.Repositories;
using MauiAdvices.Infrastructure.Persistence.Database;
using MauiAdvices.Infrastructure.Persistence.Repositories;
using MauiAdvices.Infrastructure.Services;
using MauiAdvices.Interactors.Usecases;
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
        services.AddTransient<IAdviceRepository, AdviceRepository>();
        services.AddScoped<AdviceService>();
        services.AddScoped<TranslatorService>();
        services.AddSingleton<AdviceUsecase>();

        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<AdvicesDatabase>(provider => new AdvicesDatabase(connectionString));

        return services;
    }
}