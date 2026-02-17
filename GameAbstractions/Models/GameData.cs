namespace GameAbstractions.Models
{
    public class GameData
    {
        public List<PokemonData> Pokemon { get; set; } = new();
        public List<TrainerData> Trainer { get; set; } = new();
        public List<WildEncounterData> WildEncounters { get; set; } = new();
        public List<StarterData> Starters { get; set; } = new();
        public List<ItemData> Items { get; set; } = new();
    }
}
