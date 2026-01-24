using System.Xml;
using G00DS0ULRPG.Models;
using G00DS0ULRPG.Models.Shared;

namespace G00DS0ULRPG.Services.Factories
{
    internal static class QuestFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Quest.xml";
        private static readonly List<Quest> _quest = [];

        static QuestFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }

        }

        private static void LoadQuestsFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems = new List<ItemQuantity>();

                foreach (XmlNode childNode in node.SelectNodes("./ItemsToComplete/Item"))
                {

                    var item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));

                    itemsToComplete.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }

                foreach (XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    var item = ItemFactory.CreateGameItem( childNode.AttributeAsInt("ID"));

                    rewardItems.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));

                }

                _quest.Add(new Quest(node.AttributeAsInt("ID"), node.SelectSingleNode("./Name")?.InnerText ?? "", node.SelectSingleNode("./Description")?.InnerText ?? "", itemsToComplete, node.AttributeAsInt("RewardExperiencePoints"), node.AttributeAsInt("RewardGold"), rewardItems));
            }
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }

    }
}
