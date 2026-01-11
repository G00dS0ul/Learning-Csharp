using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Engine.Shared;
using Engine.Models;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";
        private static readonly List<Monster> _baseMonsters = [];

        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
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

        private static void LoadMonsterFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                var monster = new Monster(
                    node.AttributeAsInt("ID"),
                    node.AttributeAsString("Name"),
                    $".{rootImagePath}{node.AttributeAsString("ImageName")}",
                    node.AttributeAsInt("MaximumHitPoints"),
                    ItemFactory.CreateGameItem(node.AttributeAsInt("WeaponID")),
                    node.AttributeAsInt("RewardXP"),
                    node.AttributeAsInt("Gold"));

                XmlNodeList lootItemNodes = node.SelectNodes("./LootItems/LootItem");
                if (lootItemNodes != null)
                {
                    foreach (XmlNode lootNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(
                            lootNode.AttributeAsInt("ID"),
                            lootNode.AttributeAsInt("Percentage"));
                    }
                }
                _baseMonsters.Add(monster);
            }
        }

        public static Monster GetMonster(int id)
        {
            return _baseMonsters.FirstOrDefault(m => m.ID == id)?.GetNewInstance();
        }
      
        
    }
}
