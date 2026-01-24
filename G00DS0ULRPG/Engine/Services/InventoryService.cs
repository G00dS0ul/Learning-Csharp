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

        public static Inventory AddItems(this Inventory inventory, IEnumerable<GameItem> items)
        {
            return new Inventory(inventory.Items.Concat(items));
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

       
    }
}
