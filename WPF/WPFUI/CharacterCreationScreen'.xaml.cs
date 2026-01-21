using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Engine.Services;
using Engine.Models;
using Engine.Services;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for CharacterCreationScreen_.xaml
    /// </summary>
    public partial class CharacterCreationScreen_ : Window
    {
        private GameDetails _gameDetails;
        public CharacterCreationScreen_()
        {
            InitializeComponent();

            _gameDetails = GameDetailsService.ReadGameDetails();
            DataContext = _gameDetails;
        }

        private void RandomPlayer_OnCLick(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
