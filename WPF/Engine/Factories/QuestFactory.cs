using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quest = [];

        static QuestFactory()
        {
            {
                var itemsToComplete1 = new List<ItemQuantity>();
                var rewardItems1 = new List<ItemQuantity>();

                itemsToComplete1.Add(new ItemQuantity(9001, 5));
                itemsToComplete1.Add(new ItemQuantity(9002, 10));
                rewardItems1.Add(new ItemQuantity(1003, 1));

                _quest.Add(new Quest(1, "Clear the herb garden", "Defeat the snake in the Herbalist's Garden", itemsToComplete1, 25, 10, rewardItems1));

                var itemsToComplete2 = new List<ItemQuantity>();
                var rewardItems2 = new List<ItemQuantity>();

                itemsToComplete2.Add(new ItemQuantity(9003, 3));
                itemsToComplete2.Add(new ItemQuantity(9004, 5));
                rewardItems2.Add(new ItemQuantity(1003, 1));
                _quest.Add(new Quest(2, "Clear the Farmer's Field", "Defeat the Rats in the Farmer's Field", itemsToComplete2, 25, 10, rewardItems2));

                var itemsToComplete3 = new List<ItemQuantity>();
                var rewardItems3 = new List<ItemQuantity>();

                itemsToComplete3.Add(new ItemQuantity(9005, 3));
                itemsToComplete3.Add(new ItemQuantity(9006, 5));
                rewardItems3.Add(new ItemQuantity(1003, 1));
                rewardItems3.Add(new ItemQuantity(1002, 1));
                _quest.Add(new Quest(3, "Clear the Spider Forest", "Defeat the Spiders in the Forest", itemsToComplete3, 30, 50, rewardItems3));

            }
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }

    }
}
