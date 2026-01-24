using System.Xml;
using G00DS0ULRPG.Models;
using G00DS0ULRPG.Models.Shared;

namespace G00DS0ULRPG.Services.Factories
{
    public static class WorldFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Location.xml";
        public static World CreateWorld()
        {
            var world = new World();

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                var rootImagePath = data.SelectSingleNode("/Locations")
                                        .AttributeAsString("RootImagePath");

                LoadLocationFromNodes(world, rootImagePath, data.SelectNodes("/Locations/Location"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: '{GAME_DATA_FILENAME}'");
            }

            return world;


        }

        private static void LoadLocationFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                var location = new Location(node.AttributeAsInt("X"),
                    node.AttributeAsInt("Y"),
                    node.AttributeAsString("Name"),
                    node.SelectSingleNode("./Description")?.InnerText ?? "",
                    $".{rootImagePath}{node.AttributeAsString("ImageName")}");

                AddMonsters(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));

                world.AddLocation(location);
            }
        }

        private static void AddMonsters(Location location, XmlNodeList monsters)
        {
            if (monsters == null)
            {
                return;
            }

            foreach (XmlNode monsterNode in monsters)
            {
                location.AddMonster(monsterNode.AttributeAsInt("ID"),
                    monsterNode.AttributeAsInt("Percent"));
            }
        }

        private static void AddQuests(Location location, XmlNodeList quests)
        {
            if (quests == null)
            {
                return;
            }

            foreach (XmlNode questNode in quests)
            {
                location.QuestAvailableHere.Add(QuestFactory.GetQuestByID(questNode.AttributeAsInt("ID")));
            }
        }

        private static void AddTrader(Location location, XmlNode traderHere)
        {
            if (traderHere == null)
            {
                return;
            }

            location.TraderHere = TraderFactory.GetTraderByID(traderHere.AttributeAsInt("ID"));
        }
    }

}
