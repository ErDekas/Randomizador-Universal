namespace Randomization
{
    public class RandomizationOptions
    {
        public int Seed { get; set; }

        public bool RandomizeBaseStats { get; set; } = true;
        public bool RandomizeTypes { get; set; } = false;
        public bool RandomizeAbilities { get; set; } = false;
        public bool RandomizeStarters { get; set; } = false;
        public bool RandomizeLevelUpMoves { get; set; } = false;

    }

}
