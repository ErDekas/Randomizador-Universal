using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

public partial class MainWindow : Window
{
    private readonly RomManager _romManager;

    public MainWindow()
    {
        InitializeComponent();

        _romManager = App.ServiceProvider.GetRequiredService<RomManager>();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void OpenRom(string path)
    {
        var meta = _romManager.Detect(path);
        var data = _romManager.Extract(path);

        MessageBox.Show($"Juego detectado: {meta.Title}");
    }
}
