using System.Xml.Serialization;

namespace RolePlayingGame
{
    public class Shop
    {
       public List<Item> Stocks = new List<Item>();
        public string ShopName { get; set; }
        public float SellingRate { get; set; }

        public Shop()
        {
            Stocks = new List<Item>();
            ShopName = "Inner Circle";

            Stocks.Add(new Item("Golden Sword", "Weapon", 20, 50));
            Stocks.Add(new Item("Golden Shield", "Armor", 25, 40));
            Stocks.Add(new Item("Full Health Portion", "Portion", 100, 45));

        }

        public void DisplayItems()
        {
            Console.WriteLine($"Welcome to the {ShopName}!!!");
            for (int i = 0; i < Stocks.Count; i++)
            {
                Console.WriteLine($"{1 + i}. {Stocks[i].Name} - {Stocks[i].Price}.");
            }
           
           
        }

        public void BuyItem(Player player, Item choice)
        {
            if (player.Gold >= choice.Price)
            {
                player.Gold -= choice.Price;
                var inventory = player.Inventory;
                inventory.AddItem(choice);
                Stocks.Remove(choice);
                Console.WriteLine($"{player.Name} Purchase {choice.Name} for {choice.Price} Gold. Remaining {player.Gold} Gold");
            }

            else
            {
                Console.WriteLine("Not Enough Gold");
            }
            
        }

        public void SellItem(Player player, Item choice)
        {
            player.Gold += choice.Price * SellingRate;
            var inventory = player.Inventory;
            inventory.RemoveItem(choice);
            Stocks.Add(choice);
            Console.WriteLine($"{player.Name} Sold an Item from Invetory, Gold increased by {player.Gold}.");
        }
    }
}