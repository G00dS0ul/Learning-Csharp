namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public GameItem(int itemTypeID, string name, int price)
        {
            this.ItemTypeID = itemTypeID;
            this.Name = name;
            this.Price = price;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price);
        }
    }
}
