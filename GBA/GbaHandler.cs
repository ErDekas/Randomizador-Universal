using GameAbstractions.Interfaces;
using GameAbstractions.Models;

namespace GBA
{
    public class GbaHandler : IGameHandler
    {
        public bool CanHandle(string romPath)
        {
            // Implement logic to check if the ROM is a GBA game
            return Path.GetExtension(romPath)
                .Equals(".gba", StringComparison.OrdinalIgnoreCase);
        }
        public GameMetaData Detect(string romPath)
        {
            // Implement logic to detect game metadata from the ROM
            return new GameMetaData
            {
                Title = "Example GBA Game",
                Code = "XXXX",
                Generation = 3
            };
        }
        public GameData Extract(string romPath)
        {
            // Implement logic to extract game data from the ROM
            return new GameData();
        }
        public void Rebuild(GameData data, string outputPath)
        {
            // Implement logic to rebuild the ROM from the modified game data
        }
    }
}
