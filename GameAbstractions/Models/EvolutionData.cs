namespace GameAbstractions.Models
{
    public class EvolutionData
    {
        public int Method { get; set; }     // método de evolución (level, stone, trade...)
        public int Parameter { get; set; }  // nivel o id de objeto
        public int TargetSpeciesId { get; set; }
    }

}
