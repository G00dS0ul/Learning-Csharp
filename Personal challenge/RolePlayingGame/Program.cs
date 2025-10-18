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

            var gameManager = new GameManager(hero, shop, enemy);

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