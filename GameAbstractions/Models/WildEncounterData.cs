namespace GameAbstractions.Models
{
    public class WildEncounterData
    {
        public int MapId { get; set; }
        public List<WildSlot> Slots { get; set; } = new();
    }
}
