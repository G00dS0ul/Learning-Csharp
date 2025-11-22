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
            Stocks.Add(new Item("Dragon Scale Shield", "Armor", 40, 50));
            Stocks.Add(new Item("Flame Sword", "Weapon", 50, 60));

        }

        public void DisplayItems()
        {
            ConsoleUI.PrintColor($"Welcome to the {ShopName}!!!", ConsoleColor.Cyan);
            for (int i = 0; i < Stocks.Count; i++)
            {
                ConsoleUI.PrintColor($"{1 + i}. {Stocks[i].Name} - {Stocks[i].Price}.", ConsoleColor.Magenta);
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
                ConsoleUI.PrintColor($"{player.Name} Purchase {choice.Name} for {choice.Price} Gold. Remaining {player.Gold} Gold", ConsoleColor.Green);
            }

            else
            {
                ConsoleUI.PrintColor("Not Enough Gold", ConsoleColor.Red);
            }
            
        }

        public void SellItem(Player player, Item choice)
        {
            player.Gold += choice.Price * SellingRate;
            var inventory = player.Inventory;
            inventory.RemoveItem(choice);
            Stocks.Add(choice);
            ConsoleUI.PrintColor($"{player.Name} Sold an Item from Invetory, Gold increased by {player.Gold}.", ConsoleColor.Magenta);
        }
    }
}