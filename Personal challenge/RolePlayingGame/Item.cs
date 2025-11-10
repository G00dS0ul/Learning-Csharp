namespace RolePlayingGame
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public float Price { get; set; }


        public Item(string name, string type, int value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }
        public Item(string name, string type, int value, int price)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
            this.Price = price;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - Value: {Value}";
        }
    }
}