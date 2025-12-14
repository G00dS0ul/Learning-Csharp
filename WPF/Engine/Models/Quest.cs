using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemQuantity> ItemToComplete { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; set; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemToComplete, int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.ItemToComplete = itemToComplete;
            this.RewardItems = rewardItems;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.RewardGold = rewardGold;
        }
    }
}
