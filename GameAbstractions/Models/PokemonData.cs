namespace GameAbstractions.Models
{
    public class PokemonData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseSpecialAttack { get; set; }
        public int BaseSpecialDefense { get; set; }
        public int BaseSpeed { get; set; }

        public int Type1 { get; set; }
        public int Type2 { get; set; }
    }
}
