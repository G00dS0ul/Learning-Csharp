namespace RolePlayingGame
{
    public class GameManager
    {
        private Player hero;
        private Shop shop;

        public GameManager(Player player, Shop shop)
        {
            this.hero = player;
            this.shop = shop;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n ==== MainMenu ===");
                Console.WriteLine("1. View Inventory");
                Console.WriteLine("2. Visit Shop");
                Console.WriteLine("3. Equip Item");
                Console.WriteLine("4. Exit");

                Console.Write("Choose");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        hero.Inventory.ShowInventory();
                        break;
                    case "2":
                        VisitShop();
                        break;
                    case "3":
                        EquipItem();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Enter input again!!!");
                        break;


                }    
            }

        }

        private void VisitShop()
        {
            shop.DisplayItems();
            Console.WriteLine($"{hero.Name} has {hero.Gold} Gold");
            Console.Write("Enter 1 to Buy an Item or 2 to Sell an Item: ");
            var option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Select an Item you want to buy(0 to cancel): ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= shop.Stocks.Count)
                {
                    var item = shop.Stocks[choice];
                    shop.BuyItem(hero, item);
                }

                else
                {
                    Console.WriteLine("Invalid Input!!!");
                }       
            }
            else if (option == "2")
            {
                var inventory = new Inventory();
                inventory.ShowInventory();
                Console.Write("Select an item you want to Sell from your Inventory: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= inventory.items.Count)
                {
                    var item = inventory.items[choice];
                    shop.SellItem(hero, item);
                }

                else
                {
                    Console.WriteLine("Invalid Input");
                }

            }
        }

        private void EquipItem()
        {
            hero.Inventory.ShowInventory();
            Console.Write("Choose Item you want to Equip(0 to cancel): ");

            var inventory = new Inventory();
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= inventory.items.Count)
            {
                hero.EquipItem(inventory.items[choice]);
            }
        }
    }
}