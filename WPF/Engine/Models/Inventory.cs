using System.Collections.Generic;
using System.Linq;
using Engine.Services;

namespace Engine.Models
{
    public class Inventory
    {
        #region Backing Variables

        private readonly List<GameItem> _backingInventory = [];
        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems = [];

        #endregion

        #region Properties

        public IReadOnlyList<GameItem> Items => _backingInventory.AsReadOnly();
        public IReadOnlyList<GroupedInventoryItem> GroupedItems => _backingGroupedInventoryItems.AsReadOnly();

        public IReadOnlyList<GameItem> Weapons =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Weapon).AsReadOnly();
        public IReadOnlyList<GameItem> Consumables => _backingInventory.ItemsThatAre(GameItem.ItemCategory.Consumable).AsReadOnly();
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
                if (_backingGroupedInventoryItems.All(gi => gi.Item.ItemTypeID != item.ItemTypeID))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }

                _backingGroupedInventoryItems.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
        }

        #endregion
    }
}
