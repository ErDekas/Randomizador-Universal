namespace GameAbstractions.Models
{
    public class TrainerData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TrainerPokemon> Team { get; set; } = new();
    }
}
