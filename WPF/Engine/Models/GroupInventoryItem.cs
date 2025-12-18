namespace Engine.Models
{
    public class GameItem : BaseNotificationClass
    {
        private GameItem? _item;
        private int _quantity;

        public GameItem? Item
        {
            get 
            { 
                return _item; 
            }
            set 
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public int Quantity
        {
            get 
            { 
                return _quantity; 
            }
            set 
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public GameItem(GameItem item, int quantity)
        {
            this.Item = item;
            this.Quantity = quantity;
        }
    }
}
