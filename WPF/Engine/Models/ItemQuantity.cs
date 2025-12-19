namespace Engine.Models
{
    public class ItemQuantity
    {
        public int ItemID { get; }
        public int Quantity { get; }

        public ItemQuantity(int itemID, int quantity)
        {
            this.ItemID = itemID;
            this.Quantity = quantity;
        }
    }
}
