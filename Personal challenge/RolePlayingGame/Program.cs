using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RolePlayingGame
{
    public class SaveData
    {
        public string Name;
        public int Level;
        public int Experience;
        public int ExperienceToNextLevel;
        public int Strength;
        public int Defence;
        public int Gold;
        public List<Item> InventoryItems;
        public Item EquippedWeapon;
        public Item EquippedArmor;
        public Item EquippedPotion;


        public void SaveGame()
        {
            var saveData = new SaveData();
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            var enemySpawner = new EnemySpawner();
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