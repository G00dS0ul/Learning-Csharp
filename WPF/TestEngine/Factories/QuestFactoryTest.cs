using Engine.Factories;

namespace TestEngine.Factories;

[TestClass]
public class QuestFactoryTest
{
    [TestMethod]
    public void GetQuestByID_WithValidID1_ReturnHerbGardenQuest()
    {
        var quest = QuestFactory.GetQuestByID(1);

        Assert.IsNotNull(quest);
        Assert.AreEqual(1, quest.ID);
        Assert.AreEqual("Clear the herb garden", quest.Name);
        Assert.AreEqual(25, quest.RewardExperiencePoints);
    }

    [TestMethod]
    public void GetQuestByID_WithValidID3_ReturnTownGateQuest()
    {
        var quest = QuestFactory.GetQuestByID(3);

        Assert.IsNotNull(quest);
        Assert.AreEqual(3, quest.ID);
        Assert.AreEqual("Clear the Spider Forest", quest.Name);
        Assert.AreEqual(50, quest.RewardGold);
    }

    [TestMethod]
    public void GetQuestByID_WithInvalidID_ReturnsNull()
    {
        var quest = QuestFactory.GetQuestByID(99);

        Assert.IsNull(quest);
    }

    [TestMethod]
    public void GetQuestByID_WithZeroID_ReturnsNull()
    {
        var quest = QuestFactory.GetQuestByID(0);

        Assert.IsNull(quest);
    }

    [TestMethod]
    public void GetQuestByID_WithNegativeID_ReturnsNull()
    {
        var quest = QuestFactory.GetQuestByID(-1);

        Assert.IsNull(quest);
    }

    [TestMethod]
    public void GetQuestByID_Quest1_HasCorrectItemsToComplete()
    {
        var quest = QuestFactory.GetQuestByID(1);
        
        Assert.IsNotNull(quest.ItemToComplete);
        Assert.AreEqual(2, quest?.ItemToComplete.Count);
        Assert.IsTrue(quest.ItemToComplete.Any(i => i.ItemID == 9001 && i.Quantity == 5));
        Assert.IsTrue(quest.ItemToComplete.Any(i => i.ItemID == 9002 && i.Quantity == 10));
    }

    [TestMethod]
    public void GetQuestByID_Quest1_HasCorrectRewardItems()
    {
        var quest = QuestFactory.GetQuestByID(1);

        Assert.IsNotNull(quest.RewardItems);
        Assert.AreEqual(1, quest?.RewardItems.Count);
        Assert.IsTrue(quest.RewardItems.Any(i => i.ItemID == 1003 && i.Quantity == 1));
    }

    [TestMethod]
    public void GetQuestByID_Quest3_HasMultipleRewardItems()
    {
        var quest = QuestFactory.GetQuestByID(3);

        Assert.IsNotNull(quest.RewardItems);
        Assert.AreEqual(2, quest?.RewardItems.Count);
        Assert.IsTrue(quest.RewardItems.Any(i => i.ItemID == 1002));
        Assert.IsTrue(quest.RewardItems.Any(i => i.ItemID == 1003));

        
    }

    [TestMethod]
    public void GetQuestByID_CalledMultipleTimes_ReturnsSameInstance()
    {
        var quest1 = QuestFactory.GetQuestByID(2);
        var quest2 = QuestFactory.GetQuestByID(2);

        Assert.AreSame(quest1, quest2);
    }
}