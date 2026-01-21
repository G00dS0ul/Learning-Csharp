using Engine.Services;

namespace TestEngine.Services;

[TestClass]
public class TestSaveGameService
{
    [TestMethod]
    public void Test_Restore_0_1_000()
    {
        var gameSession = SaveGameService.LoadLastSaveOrCreateNew(@".\TestFiles\SavedGames\Game_0_1_000.g00ds0ulrpg");

        Assert.AreEqual("0.1.000", gameSession.GameDetails.Version);
        Assert.AreEqual(-1, gameSession.CurrentLocation.XCoordinate);
        Assert.AreEqual(-1, gameSession.CurrentLocation.YCoordinate);

        Assert.AreEqual("Fighter", gameSession.CurrentPlayer.CharacterClass);
        Assert.AreEqual("G00dS0ul", gameSession.CurrentPlayer.Name);
        Assert.AreEqual(16, gameSession.CurrentPlayer.Dexterity);
        Assert.AreEqual(9, gameSession.CurrentPlayer.CurrentHitPoints);
        Assert.AreEqual(10, gameSession.CurrentPlayer.MaximumHitPoints);
        Assert.AreEqual(0, gameSession.CurrentPlayer.ExperiencePoints);
        Assert.AreEqual(1, gameSession.CurrentPlayer.Level);
        Assert.AreEqual(999924, gameSession.CurrentPlayer.Gold);

        Assert.HasCount(3, gameSession.CurrentPlayer.Quests);
        Assert.AreEqual(3, gameSession.CurrentPlayer.Quests[2].PlayerQuest.ID);
        Assert.IsFalse(gameSession.CurrentPlayer.Quests[0].IsComplete);

        Assert.HasCount(1, gameSession.CurrentPlayer.Recipes);
        Assert.AreEqual(1, gameSession.CurrentPlayer.Recipes[0].ID);

        Assert.HasCount(13, gameSession.CurrentPlayer.Inventory.Items);
        Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals((1001))));
        Assert.AreEqual(10, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals((2001))));
        Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals((3001))));
        Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals((1003))));

    }
}
