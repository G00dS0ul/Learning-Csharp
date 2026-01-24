using G00DS0ULRPG.Models.Shared;
using Newtonsoft.Json;

namespace G00DS0ULRPG.Models
{
    public class Inventory
    {
        #region Backing Variables

        private readonly List<GameItem> _backingInventory = [];
        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems = [];

        #endregion

        #region Properties

        public IReadOnlyList<GameItem> Items => _backingInventory.AsReadOnly();
        [JsonIgnore]
        public IReadOnlyList<GroupedInventoryItem> GroupedItems => _backingGroupedInventoryItems.AsReadOnly();
        [JsonIgnore]
        public IReadOnlyList<GameItem> Weapons =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Weapon).AsReadOnly();
        [JsonIgnore]
        public IReadOnlyList<GameItem> Consumables => _backingInventory.ItemsThatAre(GameItem.ItemCategory.Consumable).AsReadOnly();
        [JsonIgnore]
        public bool HasConsumable => Consumables.Any();

        #endregion

        #region Constructors

        public Inventory(IEnumerable<GameItem>? items = null)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                _backingInventory.Add(item);

                AddItemToGroupedInventory(item);
            }
        }

        #endregion

        #region Public Functions

        public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
        {
            return items.All(item => Items.Count(i => i.ItemTypeID == item.ItemID) >= item.Quantity);
        }

        public Inventory AddItem(GameItem item)
        {
            return AddItems(new List<GameItem> { item });
        }

        public  Inventory AddItems(IEnumerable<GameItem> items)
        {
            return new Inventory(Items.Concat(items));
        }

        public Inventory RemoveItem(GameItem item)
        {
            return RemoveItems(new List<GameItem> { item });
        }

        public Inventory RemoveItems(IEnumerable<GameItem> items)
        {
            var workingInventory = Items.ToList();
            IEnumerable<GameItem> itemsToRemove = items.ToList();

            foreach (var item in itemsToRemove)
            {
                workingInventory.Remove(item);
            }

            return new Inventory(workingInventory);
        }

        public Inventory RemoveItems(IEnumerable<ItemQuantity> itemQuantities)
        {
            var workingInventory = Items.ToList();

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

        #endregion

        #region Private Functions

        private void AddItemToGroupedInventory(GameItem item)
        {
            if (item.IsUnique)
            {
                _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (_backingGroupedInventoryItems.All(gi => gi.Item?.ItemTypeID != item.ItemTypeID))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }

                _backingGroupedInventoryItems.First(gi => gi.Item?.ItemTypeID == item.ItemTypeID).Quantity++;
            }
        }

        #endregion
    }
}
