using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quest = new List<Quest>();

        static QuestFactory()
        {
            {
                List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems = new List<ItemQuantity>();

                itemsToComplete.Add(new ItemQuantity(9001, 5));
                rewardItems.Add(new ItemQuantity(1002, 1));

                _quest.Add(new Quest(1, "Clear the herb garden", "Defeat the snake in the Herbalist's Garden", itemsToComplete, 25, 10, rewardItems));
            }
        }

        internal static Quest GetQuestByID(int id)
        {
            return _quest.FirstOrDefault(quest => quest.ID == id);
        }

    }
}
