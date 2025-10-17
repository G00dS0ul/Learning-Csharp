using System.ComponentModel.DataAnnotations;
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
                Name = "G00dS0ul",
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

            var gameManager = new GameManager(hero, shop);

            Item sword = new Item("Excalibur", "Weapon", 5);
            Item shield = new Item("Bronze Shield", "Armor", 10);
            Item potion = new Item("Healing Potion", "Portion", 20);

            hero.Inventory.AddItem(sword);
            hero.Inventory.AddItem(potion);
            hero.Inventory.AddItem(shield);

            gameManager.Start();

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

            gameManager.Start();

            Thread.Sleep(1000);

            hero.Inventory.ShowInventory();

            Thread.Sleep(1000);
            shop.DisplayItems();

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

            Thread.Sleep(1000);

            hero.Inventory.ShowInventory();

            shop.DisplayItems();

            gameManager.Start();
        }
    }
}