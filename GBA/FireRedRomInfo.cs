namespace GBA
{
    public static class FireRedRomInfo
    {
        // Base Stats
        public const int PokemonCount = 386;
        public const int BaseStatsOffset = 0x25480C;
        public const int BaseStatsSize = 0x1C; // 28 bytes por Pokémon

        // Evolutions
        public const int EvolutionsOffset = 0x2597EC;
        public const int EvolutionsPerPokemon = 5;
        public const int EvolutionSize = 8; // 8 bytes por entrada

        // Level Up Moves
        public const int LevelUpMovesPointerTable = 0x25D7B4;

        // Starters
        public const int StarterOffset = 0x169BB4;
    }

}
