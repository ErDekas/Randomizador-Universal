namespace GameAbstractions.Models
{
    public class PokemonData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Base stats
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int BaseSpecialAttack { get; set; }
        public int BaseSpecialDefense { get; set; }
        public int BaseSpeed { get; set; }

        // Tipos
        public int Type1 { get; set; }
        public int Type2 { get; set; }

        // Nuevos campos importantes
        public int CatchRate { get; set; }
        public int BaseExpYield { get; set; }

        public int Ability1 { get; set; }
        public int Ability2 { get; set; }

        public int GenderRatio { get; set; }

        public List<EvolutionData> Evolutions { get; set; } = new();
        public List<LevelUpMove> LevelUpMoves { get; set; } = new();
    }

}
