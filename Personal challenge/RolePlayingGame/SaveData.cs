using System.Text.Json;
using System.Text.Json.Serialization;


namespace RolePlayingGame
{
    public class SaveData
    {
        private const string SaveFolder = "Saves";

        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public float Gold { get; set; }
        [JsonInclude]
        public List<Item> InventoryItems { get; set; } = new();
        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }
        public Item EquippedPotion { get; set; }


        public void SaveGame(Player hero, string fileName)
        {
            if (!Directory.Exists(SaveFolder))
                Directory.CreateDirectory(SaveFolder);

            


            string path = Path.Combine(SaveFolder, fileName + ".json");

            var saveData = new SaveData
            {
                Name = hero.Name,
                Level = hero.Level,
                Experience = hero.Experience,
                ExperienceToNextLevel = hero.ExperienceToNextLevel,
                Strength = hero.Strength,
                Defence = hero.Defence,
                Health = hero.Health,
                Gold = hero.Gold,
                InventoryItems = new List<Item>(hero.Inventory.items),
                EquippedArmor = hero.EquippedArmor,
                EquippedPotion = hero.EquippedPortion,
                EquippedWeapon = hero.EquippedWeapon

            };

            

            string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(path, json);
            ConsoleUI.PrintColor("Game Saved Successfully", ConsoleColor.DarkGreen);

        }

        public void LoadGame(Player hero, string filePath)
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonSerializer.Deserialize<SaveData>(json);

            if (data == null)
            {
                ConsoleUI.PrintColor("Invalid save File!", ConsoleColor.Red);
                return;
            }

            hero.Name = data.Name ?? "Unknown";
            hero.Level = data.Level;
            hero.Experience = data.Experience;
            hero.ExperienceToNextLevel = data.ExperienceToNextLevel;
            hero.Strength = data.Strength;
            hero.Defence = data.Defence;
            hero.Health = data.Health;
            hero.Gold = data.Gold;
            hero.EquippedWeapon = data.EquippedWeapon;
            hero.EquippedArmor = data.EquippedArmor;
            hero.EquippedPortion = data.EquippedPotion;


            if (hero.Inventory != null)
            {
                hero.Inventory.items.Clear();
                if(data.InventoryItems != null)
                {
                    hero.Inventory.items = new List<Item>(data.InventoryItems);
                }
            }

            ConsoleUI.PrintColor("Game Loaded Successfully", ConsoleColor.DarkGreen);
        }

        public void LoadGameMenu(Player hero)
        {
            if(!Directory.Exists(SaveFolder))
            {
                ConsoleUI.PrintColor("No save Directory", ConsoleColor.Red);
                return;
            }

            string[] saves = Directory.GetFiles(SaveFolder, "*json");

            if (saves.Length == 0)
            {
                ConsoleUI.PrintColor("No Save file Found", ConsoleColor.Red);
            }

            ConsoleUI.PrintColor("\n==== Avalaible Save File ====", ConsoleColor.DarkCyan);
            for (int i = 0; i < saves.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(saves[i])}");
            }

            Console.Write("Select a  file you want to load: ");
            if(int.TryParse(Console.ReadLine(), out int choice) &&  choice > 0 && choice <= saves.Length)
            {
                LoadGame(hero, saves[choice - 1]);
            }
            else
            {
                ConsoleUI.PrintColor("Invalid Selection!!", ConsoleColor.Red);
            }

        }
    }
}