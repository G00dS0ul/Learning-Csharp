namespace Engine.Models
{
    public class Quest
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }
        public List<ItemQuantity> ItemToComplete { get; }
        public int RewardExperiencePoints { get; }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; }

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
