using GameAbstractions.Models;

namespace GameAbstractions.Interfaces
{
    public interface IGameHandler
    {
        bool CanHandle(string romPath);

        GameMetaData Detect(string romPath);

        GameData Extract(string romPath);

        void Rebuild(GameData data, string outputPath);
    }
}
