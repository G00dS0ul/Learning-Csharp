using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RolePlayingGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var enemySpawner = new EnemySpawner();
            var shop = new Shop();
            Console.Write("Enter Name of Player: ");
            var playerName = Console.ReadLine();
            var hero = new Player(playerName);

            var gameManager = new GameManager(hero, shop, enemySpawner);

            Item sword = new Item("Excalibur", "Weapon", 5);
            Item shield = new Item("Bronze Shield", "Armor", 10);
            Item potion = new Item("Healing Potion", "Portion", 20);

            hero.Inventory.AddItem(sword);
            hero.Inventory.AddItem(potion);
            hero.Inventory.AddItem(shield);

            gameManager.Start();

            
        }
    }
}