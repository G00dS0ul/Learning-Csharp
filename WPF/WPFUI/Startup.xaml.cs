using System;
using System.Collections.Generic;
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

namespace WPFUI
{ 
    public partial class Startup : Window
    {
        private GameDetails _gameDetails;
        public Startup()
        {
            InitializeComponent();

            _gameDetails = GameDetailsService.ReadGameDetails();

            DataContext = _gameDetails;
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            var characterCreationWindow = new CharacterCreationScreen_();
            characterCreationWindow.Show();
            Close();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
