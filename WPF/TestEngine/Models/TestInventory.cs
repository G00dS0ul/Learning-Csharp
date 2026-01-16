using Engine.Factories;
using Engine.Models;
using Engine.Services;

namespace TestEngine.Models;

[TestClass]
public class TestInventory
{
    [TestMethod]
    public void Test_Instantiate()
    {
        var inventory = new Inventory();

        Assert.IsEmpty(inventory.Items);
    }

    [TestMethod]
    public void Test_AddItem()
    {
        var inventory = new Inventory();

        var inventory1 = inventory.AddItemFromFactory(3001);

        Assert.HasCount(1, inventory1.Items);
    }

    [TestMethod]
    public void Test_AddItems()
    {
        var inventory = new Inventory();

        var itemsToAdd = new List<GameItem>
        {
            ItemFactory.CreateGameItem(3002),
            ItemFactory.CreateGameItem(3003)
        };

        var inventory1 = inventory.AddItems(itemsToAdd);

        Assert.HasCount(2, inventory1.Items);

        var inventory2 = inventory1
            .AddItemFromFactory(3001)
            .AddItemFromFactory(3002);

        Assert.HasCount(4, inventory2.Items);

    }

    [TestMethod]
    public void Test_AddItemQuantities()
    {
        var inventory = new Inventory();

        var inventory1 = inventory.AddItems(new List<ItemQuantity> { new ItemQuantity(1001, 3) });

        Assert.AreEqual(3, inventory1.Items.Count(i => i.ItemTypeID == 1001));

        var inventory2 = inventory1.AddItemFromFactory(1001);

        Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeID == 1001));

        var inventory3 = inventory2.AddItems(new List<ItemQuantity> { new ItemQuantity(1002, 1) });

        Assert.AreEqual(4, inventory3.Items.Count(i => i.ItemTypeID == 1001));
        Assert.AreEqual(1, inventory3.Items.Count(i => i.ItemTypeID == 1002));
    }

    [TestMethod]
    public void Test_RemoveItem()
    {
        var inventory = new Inventory();

        var item1 = ItemFactory.CreateGameItem(3001);
        var item2 = ItemFactory.CreateGameItem(3001);

        var inventory1 = inventory.AddItems(new List<GameItem> { item1, item2 });

        var inventory2 = inventory1.RemoveItem(item1);

        Assert.HasCount(1, inventory2.Items);
    }

    [TestMethod]
    public void Test_RemoveItems()
    {
        var inventory = new Inventory();

        var item1 = ItemFactory.CreateGameItem(3001);
        var item2 = ItemFactory.CreateGameItem(3003);
        var item3 = ItemFactory.CreateGameItem(3003);

        var inventory1 = inventory.AddItems(new List<GameItem> { item1, item2, item3 });

        var inventory2 = inventory1.RemoveItems(new List<GameItem> { item2, item3 });

        Assert.HasCount(1, inventory2.Items);
    }

    [TestMethod]
    public void Test_CategorizedItemProperties()
    {
        var inventory = new Inventory();

        Assert.HasCount(0, inventory.Weapons);
        Assert.HasCount(0, inventory.Consumables);

        var inventory1 = inventory.AddItemFromFactory(1001) // Weapon
            .AddItemFromFactory(2001); // Consumable

        Assert.HasCount(1, inventory1.Weapons);
        Assert.HasCount(1, inventory1.Consumables);

        var inventory2 = inventory1.AddItemFromFactory(3001);

        Assert.HasCount(1, inventory2.Weapons);
        Assert.HasCount(1, inventory2.Consumables);

        var inventory3 = inventory2.AddItemFromFactory(1003);

        Assert.HasCount(2, inventory3.Weapons);
        Assert.HasCount(1, inventory3.Consumables);

        var inventory4 = inventory3.AddItemFromFactory(2001);

        Assert.HasCount(2, inventory4.Weapons);
        Assert.HasCount(2, inventory4.Consumables);
    }

    [TestMethod]
    public void Test_RemoveItemQuantities()
    {
        var inventory = new Inventory();

        Assert.HasCount(0, inventory.Weapons);
        Assert.HasCount(0, inventory.Consumables);

        var inventory2 = inventory
            .AddItemFromFactory(1001)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(3001)
            .AddItemFromFactory(3001);

        Assert.AreEqual(1, inventory2.Items.Count(i => i.ItemTypeID == 1001));
        Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeID == 1002));
        Assert.AreEqual(2, inventory2.Items.Count(i => i.ItemTypeID == 3001));

        var inventory3 = inventory2.RemoveItems(new List<ItemQuantity> { new ItemQuantity(1002, 3) });

        Assert.AreEqual(1, inventory3.Items.Count(i => i.ItemTypeID == 1001));
        Assert.AreEqual(1, inventory3.Items.Count(i => i.ItemTypeID == 1002));
        Assert.AreEqual(2, inventory3.Items.Count(i => i.ItemTypeID == 3001));

        var inventory4 = inventory3.RemoveItems(new List<ItemQuantity> { new ItemQuantity(3001, 1) });

        Assert.AreEqual(1, inventory4.Items.Count(i => i.ItemTypeID == 1001));
        Assert.AreEqual(1, inventory4.Items.Count(i => i.ItemTypeID == 1002));
        Assert.AreEqual(1, inventory4.Items.Count(i => i.ItemTypeID == 3001));
    }

    [TestMethod]
    public void Test_RemoveItemQuantities_RemoveTooMany()
    {
        var inventory = new Inventory();

        Assert.HasCount(0, inventory.Weapons);
        Assert.HasCount(0, inventory.Consumables);

        var inventory2 = inventory
            .AddItemFromFactory(1001)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(1002)
            .AddItemFromFactory(3001)
            .AddItemFromFactory(3001);

        Assert.AreEqual(1, inventory2.Items.Count(i => i.ItemTypeID == 1001));
        Assert.AreEqual(4, inventory2.Items.Count(i => i.ItemTypeID == 1002));
        Assert.AreEqual(2, inventory2.Items.Count(i => i.ItemTypeID == 3001));

        Assert.ThrowsExactly<ArgumentException>(() => inventory2.RemoveItems(new List<ItemQuantity> { new ItemQuantity(1002, 999) }));
    }

}
