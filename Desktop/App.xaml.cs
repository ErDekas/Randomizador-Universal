using Core.Services;
using Desktop;
using GameAbstractions.Interfaces;
using GBA;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

public partial class App : Application
{
    public static ServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        // Handlers
        services.AddSingleton<IGameHandler, GbaHandler>();

        // Core
        services.AddSingleton<GameDetector>();
        services.AddSingleton<RomManager>();

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
