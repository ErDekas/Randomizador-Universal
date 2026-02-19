using Core.Services;
using GameAbstractions.Interfaces;
using GameAbstractions.Models;
using GBA;
using Randomization;
using System.Windows;

namespace Desktop;

public partial class MainWindow : Window
{
    private readonly RomManager _romManager;

    public MainWindow()
    {
        InitializeComponent();

        // Registramos handlers disponibles
        var handlers = new List<IGameHandler>
        {
            new GbaHandler()
        };

        var detector = new GameDetector(handlers);
        _romManager = new RomManager(detector);
    }

    private void RandomizeButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string romPath = RomPathTextBox.Text;
            string outputPath = OutputPathTextBox.Text;

            int seed = int.Parse(SeedTextBox.Text);

            // 1️⃣ Detectar juego
            var metadata = _romManager.Detect(romPath);

            // 2️⃣ Extraer datos
            GameData gameData = _romManager.Extract(romPath);

            // 3️⃣ Crear opciones
            var options = new RandomizationOptions
            {
                Seed = seed,
                RandomizeBaseStats = BaseStatsCheckBox.IsChecked == true,
                RandomizeTypes = TypesCheckBox.IsChecked == true,
                RandomizeAbilities = AbilitiesCheckBox.IsChecked == true,
                RandomizeStarters = StartersCheckBox.IsChecked == true,
                RandomizeLevelUpMoves = MovesCheckBox.IsChecked == true
            };

            // 4️⃣ Aplicar randomización
            var engine = new RandomizationEngine(seed);
            engine.Apply(gameData, options);

            // 5️⃣ Rebuild ROM
            _romManager.Rebuild(gameData,outputPath, romPath);

            MessageBox.Show("ROM randomizada correctamente 🔥", "Éxito");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error");
        }
    }
    private void BrowseRomButton_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "GBA ROM (*.gba)|*.gba"
        };
        if (dlg.ShowDialog() == true)
            RomPathTextBox.Text = dlg.FileName;
    }

    private void BrowseOutputButton_Click(object sender, RoutedEventArgs e)
    {
        var dlg = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "GBA ROM (*.gba)|*.gba",
            FileName = "FireRed_Randomized.gba"
        };
        if (dlg.ShowDialog() == true)
            OutputPathTextBox.Text = dlg.FileName;
    }

}
