using GameAbstractions.Interfaces;

namespace Core.Services
{
    public class GameDetector
    {
        private readonly IEnumerable<IGameHandler> _handlers;
        public GameDetector(IEnumerable<IGameHandler> handlers)
        {
            _handlers = handlers;
        }
        public IGameHandler DetectHandler(string romPath)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(romPath));

            if (handler != null)
            {
                return handler;
            } else
            {
                throw new NotSupportedException($"No handler found for ROM: {romPath}");
            }
        }
    }
}
