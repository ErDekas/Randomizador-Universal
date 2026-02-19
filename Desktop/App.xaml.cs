using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Core.Services;
using GameAbstractions.Interfaces;
using GBA;

namespace Desktop;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        Services = services.BuildServiceProvider();

        var mainWindow = Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }


    private void ConfigureServices(IServiceCollection services)
    {
        // Handlers
        services.AddSingleton<IGameHandler, GbaHandler>();

        // Core services
        services.AddSingleton<GameDetector>();
        services.AddSingleton<RomManager>();

        // Main Window
        services.AddSingleton<MainWindow>();
    }
}
