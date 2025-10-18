using System;

namespace RolePlayingGame
{
    public class GameManager
    {
        private Player hero;
        private Enemy enemy;
        private Shop shop;

        public GameManager(Player player, Shop shop, Enemy enemy)
        {
            this.hero = player;
            this.shop = shop;
            this.enemy = enemy;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n ==== MainMenu ===");
                Console.WriteLine("1. Start Battle");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Visit Shop");
                Console.WriteLine("4. Equip Item");
                Console.WriteLine("5. Exit");

                Console.Write("Choose: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        StateBattle();
                        break;
                    case "2":
                        hero.Inventory.ShowInventory();
                        break;
                    case "3":
                        VisitShop();
                        break;
                    case "4":
                        EquipItem();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Enter input again!!!");
                        break;


                }    
            }

        }

        private void StateBattle()
        {
            Console.WriteLine("Battle Starts");

            while (hero.IsAlive() && enemy.IsAlive())
            {
                hero.Attack(enemy);

                if (!enemy.IsAlive())
                    break;

                enemy.Attack(hero);
            }

            Console.WriteLine(hero.IsAlive()
                ? $"{hero.Name} wins the fight"
                : $"{enemy.Name} defeat {hero.Name}!");

            Thread.Sleep(500);

            hero.Health = 100;
            enemy.Health = 100;
            Console.WriteLine($"{hero.Name}'s health has been restored to {hero.Health}!");
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
                    var item = shop.Stocks[choice - 1];
                    shop.BuyItem(hero, item);
                }

                else
                {
                    Console.WriteLine("Invalid Input!!!");
                }       
            }
            else if (option == "2")
            {
                hero.Inventory.ShowInventory();
                Console.Write("Select an item you want to Sell from your Inventory: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= hero.Inventory.items.Count)
                {
                    var item = hero.Inventory.items[choice - 1];
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

            
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= hero.Inventory.items.Count)
            {
                hero.EquipItem(hero.Inventory.items[choice - 1]);
            }
        }
    }
}