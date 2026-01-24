namespace G00DS0ULRPG.Models
{
    public class ItemQuantity
    {
        private readonly GameItem _gameitem;
        public int ItemID => _gameitem.ItemTypeID;
        public int Quantity { get; }

        public string QuantityItemDescription => $"{Quantity} {_gameitem.Name}";

        public ItemQuantity(GameItem item, int quantity)
        {
            _gameitem = item;
            Quantity = quantity;
        }
    }
}
