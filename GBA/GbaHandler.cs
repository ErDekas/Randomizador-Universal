using GameAbstractions.Interfaces;
using GameAbstractions.Models;
using System.Text;

namespace GBA
{
    public class GbaHandler : IGameHandler
    {
        public bool CanHandle(string romPath)
            => Path.GetExtension(romPath)
                .Equals(".gba", StringComparison.OrdinalIgnoreCase);

        public GameMetaData Detect(string romPath)
        {
            using var stream = new FileStream(romPath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);

            stream.Seek(0xAC, SeekOrigin.Begin);
            string code = Encoding.ASCII.GetString(reader.ReadBytes(4));

            stream.Seek(0xBC, SeekOrigin.Begin);
            byte revision = reader.ReadByte();

            if (code != "BPRE" || revision != 0)
                throw new NotSupportedException("Solo FireRed USA 1.0 soportado.");

            return new GameMetaData
            {
                Title = "Pokemon FireRed",
                Code = code,
                Generation = 3,
                Revision = revision
            };
        }

        public GameData Extract(string romPath)
        {
            var data = new GameData();

            using var stream = new FileStream(romPath, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(stream);

            ExtractBaseStats(reader, data);
            ExtractLevelUpMoves(reader, data);

            return data;
        }


        public void Rebuild(GameData data, string outputPath)
        {
            using var stream = new FileStream(outputPath, FileMode.Open, FileAccess.Write);
            using var writer = new BinaryWriter(stream);

            WriteBaseStats(writer, data);
        }



        private void ExtractBaseStats(BinaryReader reader, GameData data)
        {
            reader.BaseStream.Seek(FireRedRomInfo.BaseStatsOffset, SeekOrigin.Begin);

            for (int i = 0; i < FireRedRomInfo.PokemonCount; i++)
            {
                var pokemon = new PokemonData();
                pokemon.Id = i + 1;

                pokemon.BaseHp = reader.ReadByte();
                pokemon.BaseAttack = reader.ReadByte();
                pokemon.BaseDefense = reader.ReadByte();
                pokemon.BaseSpeed = reader.ReadByte();
                pokemon.BaseSpecialAttack = reader.ReadByte();
                pokemon.BaseSpecialDefense = reader.ReadByte();

                pokemon.Type1 = reader.ReadByte();
                pokemon.Type2 = reader.ReadByte();

                pokemon.CatchRate = reader.ReadByte();
                pokemon.BaseExpYield = reader.ReadByte();

                reader.ReadBytes(2); // EV yield

                pokemon.Ability1 = reader.ReadByte();
                pokemon.Ability2 = reader.ReadByte();

                reader.ReadBytes(12); // resto estructura no necesaria ahora

                data.Pokemon.Add(pokemon);
            }
        }
        private void ExtractStarters(BinaryReader reader, GameData data)
        {
            reader.BaseStream.Seek(FireRedRomInfo.StarterOffset, SeekOrigin.Begin);

            data.Starters.Add(new StarterData
            {
                Starter1 = reader.ReadUInt16(),
                Starter2 = reader.ReadUInt16(),
                Starter3 = reader.ReadUInt16()
            });
        }

        private void ExtractEvolutions(BinaryReader reader, GameData data)
        {
            reader.BaseStream.Seek(FireRedRomInfo.EvolutionsOffset, SeekOrigin.Begin);

            foreach (var pokemon in data.Pokemon)
            {
                for (int i = 0; i < FireRedRomInfo.EvolutionsPerPokemon; i++)
                {
                    ushort method = reader.ReadUInt16();
                    ushort parameter = reader.ReadUInt16();
                    ushort target = reader.ReadUInt16();
                    reader.ReadUInt16(); // unused

                    if (method != 0)
                    {
                        pokemon.Evolutions.Add(new EvolutionData
                        {
                            Method = method,
                            Parameter = parameter,
                            TargetSpeciesId = target
                        });
                    }
                }
            }
        }
        private void WriteBaseStats(BinaryWriter writer, GameData data)
        {
            writer.BaseStream.Seek(FireRedRomInfo.BaseStatsOffset, SeekOrigin.Begin);

            foreach (var pokemon in data.Pokemon)
            {
                writer.Write((byte)pokemon.BaseHp);
                writer.Write((byte)pokemon.BaseAttack);
                writer.Write((byte)pokemon.BaseDefense);
                writer.Write((byte)pokemon.BaseSpeed);
                writer.Write((byte)pokemon.BaseSpecialAttack);
                writer.Write((byte)pokemon.BaseSpecialDefense);

                writer.Write((byte)pokemon.Type1);
                writer.Write((byte)pokemon.Type2);

                writer.Write((byte)pokemon.CatchRate);
                writer.Write((byte)pokemon.BaseExpYield);

                writer.Write((ushort)0); // EV yield placeholder

                writer.Write((byte)pokemon.Ability1);
                writer.Write((byte)pokemon.Ability2);

                writer.Write(new byte[12]);
            }
        }
        private void ExtractLevelUpMoves(BinaryReader reader, GameData data)
        {
            reader.BaseStream.Seek(FireRedRomInfo.LevelUpMovesPointerTable, SeekOrigin.Begin);

            for (int i = 0; i < FireRedRomInfo.PokemonCount; i++)
            {
                uint pointer = reader.ReadUInt32();
                long moveTableOffset = pointer - 0x08000000;

                long returnPosition = reader.BaseStream.Position;

                reader.BaseStream.Seek(moveTableOffset, SeekOrigin.Begin);

                while (true)
                {
                    ushort level = reader.ReadUInt16();

                    if (level == 0xFFFF)
                        break;

                    ushort moveId = reader.ReadUInt16();

                    data.Pokemon[i].LevelUpMoves.Add(new LevelUpMove
                    {
                        Level = level,
                        MoveId = moveId
                    });
                }

                reader.BaseStream.Seek(returnPosition, SeekOrigin.Begin);
            }
        }
    }
}
