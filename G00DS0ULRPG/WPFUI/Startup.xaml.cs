using System.Windows;
using G00DS0ULRPG.Services;
using Microsoft.Win32;

namespace WPFUI
{ 
    public partial class Startup : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "g00ds0ulrpg";
        public Startup()
        {
            InitializeComponent();

            DataContext = GameDetailsService.ReadGameDetails();
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            var characterCreationWindow = new CharacterCreationScreen_();
            characterCreationWindow.Show();
            Close();
        }

        private void LoadGame_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = $"Save Game Files (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var gameState = SaveGameService.LoadLastSaveOrCreateNew(openFileDialog.FileName);

                var mainWindow = new MainWindow(gameState.Player,
                    gameState.XCoordinate,
                    gameState.YCoordinate);

                mainWindow.Show();
                Close();
            }
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
