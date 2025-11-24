using System.Text.Json;

namespace RolePlayingGame
{
    public class SaveData
    {
        private const string SavePathFile = "savegame.json";

        public Player Hero;

        public string Name;
        public int Level;
        public int Experience;
        public int ExperienceToNextLevel;
        public int Strength;
        public int Defence;
        public float Gold;
        public List<Item> InventoryItems;
        public Item EquippedWeapon;
        public Item EquippedArmor;
        public Item EquippedPotion;


        public void SaveGame(Player hero)
        {
            var saveData = new SaveData
            {
                Name = hero.Name,
                Level = hero.Level,
                Experience = hero.Experience,
                ExperienceToNextLevel = hero.ExperienceToNextLevel,
                Strength = hero.Strength,
                Defence = hero.Defence,
                Gold = hero.Gold,
                InventoryItems = hero.Inventory.items.ToList(),
                EquippedArmor = hero.EquippedArmor,
                EquippedPotion = hero.EquippedPortion,
                EquippedWeapon = hero.EquippedWeapon

            };

            string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(SavePathFile, json);
            ConsoleUI.PrintColor("Game Saved Successfully", ConsoleColor.DarkGreen);

        }

        public void LoadGame(Player hero)
        {
            if (!File.Exists(SavePathFile))
            {
                ConsoleUI.PrintColor("No Saved Game Found", ConsoleColor.DarkRed);
                return;
            }

            string json = File.ReadAllText(SavePathFile);
            SaveData data = JsonSerializer.Deserialize<SaveData>(json);

            if (data == null)
            {
                ConsoleUI.PrintColor("Invalid save File!", ConsoleColor.Red);
            }

            hero.Name = data.Name ?? "Unknown";
            hero.Experience = data.Experience;
            hero.ExperienceToNextLevel = data.ExperienceToNextLevel;
            hero.Strength = data.Strength;
            hero.Defence = data.Defence;
            hero.Gold = data.Gold;
            hero.EquippedWeapon = data.EquippedWeapon;
            hero.EquippedArmor = data.EquippedArmor;
            hero.EquippedPortion = data.EquippedPotion;


            if (hero.Inventory != null)
            {
                hero.Inventory.items.Clear();
                if(data.InventoryItems != null)
                {
                    hero.Inventory.items.AddRange(data.InventoryItems);

                }
            }

            ConsoleUI.PrintColor("Game Loaded Successfully", ConsoleColor.DarkGreen);


        }
    }
}