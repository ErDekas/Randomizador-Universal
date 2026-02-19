using GameAbstractions.Models;
using Randomization;

namespace Core.Services;

public class RandomizationEngine
{
    private readonly Random _rng;

    public RandomizationEngine(int seed)
    {
        _rng = new Random(seed);
    }

    public void Apply(GameData data, RandomizationOptions options)
    {
        foreach (var pokemon in data.Pokemon)
        {
            if (options.RandomizeBaseStats)
                RandomizeBaseStats(pokemon);

            if (options.RandomizeTypes)
                RandomizeTypes(pokemon);

            if (options.RandomizeAbilities)
                RandomizeAbilities(pokemon);

            if (options.RandomizeStarters)
                RandomizeStarters(data);

            if (options.RandomizeLevelUpMoves)
                RandomizeLevelUpMoves(pokemon);
        }
    }

    private void RandomizeBaseStats(PokemonData pkm)
    {
        int bst =
            pkm.BaseHp +
            pkm.BaseAttack +
            pkm.BaseDefense +
            pkm.BaseSpecialAttack +
            pkm.BaseSpecialDefense +
            pkm.BaseSpeed;

        pkm.BaseHp = RandomStat();
        pkm.BaseAttack = RandomStat();
        pkm.BaseDefense = RandomStat();
        pkm.BaseSpecialAttack = RandomStat();
        pkm.BaseSpecialDefense = RandomStat();
        pkm.BaseSpeed = RandomStat();

        NormalizeToBST(pkm, bst);
    }

    private int RandomStat()
        => _rng.Next(20, 150);

    private void NormalizeToBST(PokemonData pkm, int originalBst)
    {
        int current =
            pkm.BaseHp +
            pkm.BaseAttack +
            pkm.BaseDefense +
            pkm.BaseSpecialAttack +
            pkm.BaseSpecialDefense +
            pkm.BaseSpeed;

        double ratio = (double)originalBst / current;

        pkm.BaseHp = Clamp(pkm.BaseHp * ratio);
        pkm.BaseAttack = Clamp(pkm.BaseAttack * ratio);
        pkm.BaseDefense = Clamp(pkm.BaseDefense * ratio);
        pkm.BaseSpecialAttack = Clamp(pkm.BaseSpecialAttack * ratio);
        pkm.BaseSpecialDefense = Clamp(pkm.BaseSpecialDefense * ratio);
        pkm.BaseSpeed = Clamp(pkm.BaseSpeed * ratio);
    }

    private int Clamp(double value)
        => (int)Math.Clamp(Math.Round(value), 1, 255);

    private void RandomizeTypes(PokemonData pkm)
    {
        pkm.Type1 = _rng.Next(0, 18);
        pkm.Type2 = _rng.Next(0, 18);
    }

    private void RandomizeAbilities(PokemonData pkm)
    {
        pkm.Ability1 = _rng.Next(1, 77); // FireRed tiene 76 abilities
        pkm.Ability2 = _rng.Next(1, 77);
    }

    private void RandomizeStarters(GameData data)
    {
        // Barajar starters entre sí
        foreach (var starterSet in data.Starters)
        {
            int[] starters = { starterSet.Starter1, starterSet.Starter2, starterSet.Starter3 };
            starters = starters.OrderBy(x => _rng.Next()).ToArray();
            starterSet.Starter1 = starters[0];
            starterSet.Starter2 = starters[1];
            starterSet.Starter3 = starters[2];
        }
    }



    private void RandomizeLevelUpMoves(PokemonData pkm)
    {
        if (pkm == null || pkm.LevelUpMoves == null) return;

        // Shuffle the moves
        pkm.LevelUpMoves = [.. pkm.LevelUpMoves.OrderBy(x => _rng.Next())];
    }


}
