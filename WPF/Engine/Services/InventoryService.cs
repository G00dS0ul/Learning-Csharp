using Engine.Factories;
using Engine.Models;

namespace Engine.Services
{
    public static class InventoryService
    {
        public static Inventory AddItem(this Inventory inventory, GameItem item)
        {
            return inventory.AddItems(new List<GameItem> { item });
        }

        public static Inventory AddItemFromFactory(this Inventory inventory, int itemTypeID)
        {
            return inventory.AddItems(new List<GameItem> { ItemFactory.CreateGameItem(itemTypeID) });
        }

        public static Inventory AddItems(this Inventory inventory, IEnumerable<GameItem> items)
        {
            return new Inventory(inventory.Items.Concat(items));
        }

        public static Inventory AddItems(this Inventory inventory, IEnumerable<ItemQuantity> itemQuantities)

        {
            List<GameItem> itemsToAdd = [];

            foreach (var itemQuantity in itemQuantities)
            {
                for (var i = 0; i < itemQuantity.Quantity; i++)
                {
                    itemsToAdd.Add(ItemFactory.CreateGameItem(itemQuantity.ItemID));
                }
            }

            return inventory.AddItems(itemsToAdd);

        }

        public static Inventory RemoveItem(this Inventory inventory, GameItem item)
        {
            return inventory.RemoveItems(new List<GameItem> { item });
        }

        public static Inventory RemoveItems(this Inventory inventory, IEnumerable<GameItem> items)
        {
            var workingInventory = inventory.Items.ToList();
            IEnumerable<GameItem> itemsToRemove = items.ToList();

            foreach (var item in itemsToRemove)
            {
                workingInventory.Remove(item);
            }

            return new Inventory(workingInventory);
        }

        public static Inventory RemoveItems(this Inventory inventory, IEnumerable<ItemQuantity> itemQuantities)
        {
            var workingInventory = inventory.Items.ToList();

            foreach (var itemQuantity in itemQuantities)
            {
                var itemCount = workingInventory.Count(i => i.ItemTypeID == itemQuantity.ItemID);

                if (itemQuantity.Quantity > itemCount)
                {
                    throw new ArgumentException($"Cannot remove {itemQuantity.Quantity} of item {itemQuantity.ItemID}. Only {itemCount} available.");
                }

                for (var i = 0; i < itemQuantity.Quantity; i++)
                {
                    workingInventory.Remove(
                        workingInventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                }
            }

            return new Inventory(workingInventory);
        }

        public static List<GameItem> ItemsThatAre(this IEnumerable<GameItem> inventory, GameItem.ItemCategory category)
        {
            return inventory.Where(i => i.Category == category).ToList();
        }
    }
}
