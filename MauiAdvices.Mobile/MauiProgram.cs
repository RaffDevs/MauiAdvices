using System.Reflection;
using System.Text;
using MauiAdvices.CrossCutting;
using MauiAdvices.Mobile.Pages;
using MauiAdvices.Mobile.Src.Pages;
using MauiIcons.Core;
using MauiIcons.Cupertino;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MauiAdvices.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("MauiAdvices.Mobile.appsettings.json");
        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        var builder = MauiApp.CreateBuilder();

        var config = new ConfigurationBuilder()
        .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
        .Build();
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Configuration.AddConfiguration(config);
        var path = Path.Combine(FileSystem.Current.AppDataDirectory, builder.Configuration["Database:Name"]);
        var connectionString = $"Filename={path};" +
                               $"Connection={builder.Configuration["Database:Connection"]};" +
                               $"Password={builder.Configuration["Database:Password"]}";

        builder.UseMauiIconsCore(options =>
        {
            options.SetDefaultFontOverride(true);
            options.SetDefaultIconAutoScaling(true);
        });
        builder.UseCupertinoMauiIcons();
        builder.Services.ConfigureHttpClient(builder.Configuration);
        builder.Services.ConfigureServices(builder.Configuration);
        builder.Services.ConfigureDatabase(connectionString);
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<FavoritesPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}