using System.Security.Cryptography.X509Certificates;

namespace RolePlayingGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var hero = new Player
            {
                Name = "Iche",
                Health = 100,
                Defence = 10,
                Strength = 10,
                Level = 1
            };

            var enemy = new Enemy
            {
                Name = "Goblin",
                Type = "Super Natural",
                Health = 60,
                Strength = 15,
                Defence = 3
            };

            Item sword = new Item("Excalibur", "Weapon", 5);
            Item shield = new Item("Golden Shield", "Armor", 10);
            Item potion = new Item("Healing Potion", "Portion", 20);

            hero.Inventory.AddItem(sword);
            hero.Inventory.AddItem(potion);
            hero.Inventory.AddItem(shield);

            hero.Inventory.ShowInventory();


            hero.EquipItem(sword);
            hero.EquipItem(potion);
           

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


        }
    }
}