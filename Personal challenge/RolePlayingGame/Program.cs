using System.Security.Cryptography.X509Certificates;

namespace RolePlayingGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var shop = new Shop();
            
            var hero = new Player
            {
                Name = "Iche",
                Health = 100,
                Defence = 3,
                Strength = 3,
                Level = 1,
                Gold = 150
            };

            var enemy = new Enemy
            {
                Name = "Goblin",
                Type = "Super Natural",
                Health = 60,
                Strength = 20,
                Defence = 3
            };

            Item sword = new Item("Excalibur", "Weapon", 5);
            Item shield = new Item("Golden Shield", "Armor", 10);
            Item potion = new Item("Healing Potion", "Portion", 20);

            hero.Inventory.AddItem(sword);
            hero.Inventory.AddItem(potion);
            hero.Inventory.AddItem(shield);

            while (true)
            {
                hero.Inventory.ShowInventory();
                Console.Write($"Select Item you want to Equip(1 - {hero.Inventory.items.Count}): ");
                var choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    Console.WriteLine("No Item Equipped");
                    break;
                }

                if (choice >= 1 && choice <= hero.Inventory.items.Count)
                {
                    hero.EquipItem(hero.Inventory.items[choice - 1]);

                }

                else
                {
                    Console.WriteLine("Invalid Option");
                }
                break;
            }

            Thread.Sleep(1000);
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
            Console.WriteLine($"{hero.Name}'s health has been restored to {hero.Health}!");

            while (true)
            {
                shop.DisplayItems();
                Console.Write($"Select Item you want to purchase(1 - {shop.Stocks.Count}): ");
                var choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                    break;
                
                if(choice >= 1 && choice <= shop.Stocks.Count)
                {
                    shop.BuyItem(hero, shop.Stocks[choice - 1]);

                }

                else
                {
                    Console.WriteLine("Invalid Option");
                }
                break;
            }

            Thread.Sleep(1000);

            hero.Inventory.ShowInventory();

            Thread.Sleep(1000);
            shop.DisplayItems();

            Thread.Sleep(500);
            while (true)
            {
                hero.Inventory.ShowInventory();
                Console.Write($"Select Item you want to Equip(1 - {hero.Inventory.items.Count}): ");
                var choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    Console.WriteLine("No Item Equipped");
                    break;
                }

                if (choice >= 1 && choice <= hero.Inventory.items.Count)
                {
                    hero.EquipItem(hero.Inventory.items[choice - 1]);

                }

                else
                {
                    Console.WriteLine("Invalid Option");
                }
                break;
            }

            Thread.Sleep(500);

            Console.WriteLine("Battle Starts");

            Thread.Sleep(500);
           
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

        }
    }
}