using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.ViewModels; 

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();

            DataContext = _gameSession;
        }

        private void OnAddXpClick(object sender, RoutedEventArgs e)
        {
            if (_gameSession.CurrentPlayer != null)
            {
                _gameSession.CurrentPlayer.ExperiencePoints = _gameSession.CurrentPlayer.ExperiencePoints + 10;
            }
        }
    }
}