using System.Xml;
using G00DS0ULRPG.Models.Shared;
using G00DS0ULRPG.Models;
using G00DS0ULRPG.Services;
using G00DS0ULRPG.Core;

namespace G00DS0ULRPG.Services.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monster.xml";
        private static readonly GameDetails s_gameDetails;
        private static readonly List<Monster> s_baseMonsters = [];

        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {s_gameDetails = GameDetailsService.ReadGameDetails();
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                var rootImagePath = data.SelectSingleNode("/Monsters")
                                        .AttributeAsString("RootImagePath");

                LoadMonsterFromNodes(data.SelectNodes("/Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        public static Monster GetMonsterFromLocation(Location location)
        {
            if (!location.MonstersHere.Any())
            {
                return null;
            }

            var totalChances = location.MonstersHere.Sum(m => m.ChanceOfEncountering);
            var randomNumber = DiceService.Instance.Roll(totalChances, 1).Value;
            var runningTotal = 0;

            foreach (var monsterEncounter in location.MonstersHere)
            {
                runningTotal += monsterEncounter.ChanceOfEncountering;

                if(randomNumber <= runningTotal)
                {
                    return GetMonster(monsterEncounter.MonsterID);
                }
            }

            return GetMonster(location.MonstersHere.Last().MonsterID);
        }

        private static void LoadMonsterFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                var attributes = s_gameDetails.PlayerAttributes;

                attributes.First(a => a.Key.Equals("DEX")).BaseValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                attributes.First(a => a.Key.Equals("DEX")).ModifiedValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);

                var monster = new Monster(
                    node.AttributeAsInt("ID"), 
                    node.AttributeAsString("Name"), 
                    $".{rootImagePath}{node.AttributeAsString("ImageName")}", 
                    node.AttributeAsInt("MaximumHitPoints"), 
                    attributes, 
                    ItemFactory.CreateGameItem(node.AttributeAsInt("WeaponID")), 
                    node.AttributeAsInt("RewardXP"), 
                    node.AttributeAsInt("Gold"));

                XmlNodeList lootItemNodes = node.SelectNodes("./LootItems/LootItem");
                if (lootItemNodes != null)
                {
                    foreach (XmlNode lootItemNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(
                            lootItemNode.AttributeAsInt("ID"),
                            lootItemNode.AttributeAsInt("Percentage"));
                    }
                }
                s_baseMonsters.Add(monster);
            }
        }

        public static Monster GetMonster(int id)
        {
            var newMonster = s_baseMonsters.FirstOrDefault(m => m.ID == id).Clone();

            foreach (var itemPercentage in newMonster.LootTable)
            {
                if (DiceService.Instance.Roll(100).Value <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }
      
        
    }
}
