using Newtonsoft.Json;

namespace G00DS0ULRPG.Models
{
    public class Quest
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public string Description { get; }
        [JsonIgnore]
        public List<ItemQuantity> ItemToComplete { get; }
        [JsonIgnore]
        public int RewardExperiencePoints { get; }
        [JsonIgnore]
        public int RewardGold { get; }
        [JsonIgnore]
        public List<ItemQuantity> RewardItems { get; }
        [JsonIgnore]
        public string ToolTipContents => Description + Environment.NewLine + Environment.NewLine +
                                         "Items to complete the quest" + Environment.NewLine +
                                         "===========================" + Environment.NewLine +
                                         string.Join(Environment.NewLine,
                                             ItemToComplete.Select(i => i.QuantityItemDescription)) +
                                         Environment.NewLine + Environment.NewLine + "Reward\r\n" +
                                         "============================" + Environment.NewLine +
                                         $"{RewardExperiencePoints} Experience Points" + Environment.NewLine +
                                         $"{RewardGold} Gold Pieces" + Environment.NewLine +
                                         string.Join(Environment.NewLine,
                                             RewardItems.Select(i => i.QuantityItemDescription));

        public Quest(int id, string name, string description, List<ItemQuantity> itemToComplete, int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemToComplete = itemToComplete;
            RewardItems = rewardItems;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
        }
    }
}
