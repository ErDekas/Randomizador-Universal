using GameAbstractions.Models;

namespace Core.Services
{
    public class RomManager
    {
        private readonly GameDetector _detector;
        public RomManager(GameDetector detector)
        {
            _detector = detector;
        }

        public GameMetaData Detect(string romPath)
        {
            var handler = _detector.DetectHandler(romPath);
            return handler.Detect(romPath);
        }

        public GameData Extract(string romPath)
        {
            var handler = _detector.DetectHandler(romPath);
            return handler.Extract(romPath);
        }

        public void Rebuild (GameData data, string outputPath,string originalRomPath)
        {
            var handler = _detector.DetectHandler(originalRomPath);
            handler.Rebuild(data, outputPath, originalRomPath);
        }
    }
}
