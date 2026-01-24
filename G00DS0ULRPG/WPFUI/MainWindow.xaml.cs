using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.ViewModels;
using Engine.Models;
using Engine.Services;
using Microsoft.Win32;
using WPFUI.Windows;
using G00DS0ULRPG.Models.EventArgs;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "g00ds0ulrpg";

        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private GameSession? _gameSession;
        private readonly Dictionary<Key, Action>? _userInputActions = new Dictionary<Key, Action>();

        public MainWindow(Player player, int xLocation = 0, int yLocation = 0)
        {
            InitializeComponent();

            InitializeUserInputActions();

            SetActiveGameSessionTo(new GameSession(player, 0, 0));
        }

       
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession?.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession?.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession?.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession?.MoveSouth();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession?.AttackCurrentMonster();
        }

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession?.UseCurrentConsumable();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            if (_gameSession?.CurrentTrader != null)
            {
                var tradeScreen = new TraderScreen
                {
                    Owner = this,
                    DataContext = _gameSession
                };
                tradeScreen.ShowDialog();
            }
        }

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            var recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession?.CraftItemUsing(recipe);
        }

        private void InitializeUserInputActions()
        {
            _userInputActions?.Add(Key.W, () => _gameSession?.MoveNorth());
            _userInputActions?.Add(Key.A, () => _gameSession?.MoveWest());
            _userInputActions?.Add(Key.S, () => _gameSession?.MoveSouth());
            _userInputActions?.Add(Key.D, () => _gameSession?.MoveEast());
            _userInputActions?.Add(Key.Z, () => _gameSession?.AttackCurrentMonster());
            _userInputActions?.Add(Key.C, () => _gameSession?.UseCurrentConsumable());
            _userInputActions?.Add(Key.I, () => SetTabFocus("InventoryTabItem"));
            _userInputActions?.Add(Key.Q, () => SetTabFocus("QuestTabItem"));
            _userInputActions?.Add(Key.R, () => SetTabFocus("RecipesTabItem"));
            _userInputActions?.Add(Key.T, () => OnClick_DisplayTradeScreen(this, new RoutedEventArgs()));
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key))
            {
                _userInputActions[e.Key].Invoke();
            }
        }

        private void SetTabFocus(string tabName)
        {
            foreach (var item in PlayerDataTabControl.Items)
            {
                if (item is TabItem tabItem)
                {
                    if (tabItem.Name == tabName)
                    {
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }

        

        private void SetActiveGameSessionTo(GameSession gameSession)
        {
            _messageBroker.OnMessageRaised -= OnGameMessageRaised;

            _gameSession = gameSession;
            DataContext = _gameSession;

            GameMessages.Document.Blocks.Clear();

            _messageBroker.OnMessageRaised += OnGameMessageRaised;
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            var startup = new Startup();
            startup.Show();
            Close();
        }

        private void SaveGame_OnClick(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            AskToSaveGame();
        }

        private void AskToSaveGame()
        {
            var message = new YesNoWindow("Save Game", "Do you want to save your game?")
            {
                Owner = GetWindow(this)
            };
            message.ShowDialog();

            if (message.ClickedYes)
            {
                SaveGame();
            }
        }


        private void SaveGame()
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = $"Save game (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveGameService.Save(_gameSession, saveFileDialog.FileName);
            }
        }
    }
}