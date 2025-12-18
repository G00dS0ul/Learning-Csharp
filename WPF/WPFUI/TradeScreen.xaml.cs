using Engine.ViewModels;
using Engine.Models;
using System.Windows;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TraderScreen.xaml
    /// </summary>
    public partial class TraderScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public TraderScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if (item != null)
            {
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if(item != null)
            {
                if(Session.CurrentPlayer.Gold >= item.Price)
                {
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                    Session.CurrentPlayer.AddItemToInventory(item);
                }
                else
                {
                    MessageBox.Show("You do not Have Enough Gold For This Item!!!");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
