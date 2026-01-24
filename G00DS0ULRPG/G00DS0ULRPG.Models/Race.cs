namespace G00DS0ULRPG.Models
{
    public class Race
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public List<PlayerAttributeModifier> PlayerAttributeModifiers { get; } = [];
    }
}
