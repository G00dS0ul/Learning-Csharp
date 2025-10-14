namespace RolePlayingGame
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }

        public Item(string name, string type, int value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - Value: {Value}";
        }
    }
}