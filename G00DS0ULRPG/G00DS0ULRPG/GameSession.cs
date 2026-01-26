using System.Collections.ObjectModel;
using System.ComponentModel;
using G00DS0ULRPG.Models;
using G00DS0ULRPG.Services.Factories;
using G00DS0ULRPG.Services;
using Newtonsoft.Json;
using G00DS0ULRPG.Core;

namespace G00DS0ULRPG.ViewModel
{
    public class GameSession : INotifyPropertyChanged, IDisposable
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();


        #region Properties
            
        private GameDetails? _gameDetails;
        private Player? _currentPlayer;
        private Location? _currentLocation;
        private Battle _currentBattle;
        private Monster? _currentMonster;
        private Trader? _currentTrader;

        public event PropertyChangedEventHandler? PropertyChanged;

        [JsonIgnore] public GameDetails GameDetails { get; private set; }

        public Player CurrentPlayer
        {

            get => _currentPlayer;
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnPlayerKilled;
                }

                _currentPlayer = value;

                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnPlayerKilled;
                }
            }

        }

        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;

                CompleteQuestAtLocation();
                GivePlayerQuestAtLocation();
                CurrentMonster = MonsterFactory.GetMonsterFromLocation(CurrentLocation);

                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        [JsonIgnore]
        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                    _currentBattle = null;
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(CurrentPlayer, CurrentMonster);

                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;

                    _currentMonster = value;
                }

            }
        }

        [JsonIgnore] public Trader CurrentTrader { get; private set; }

        [JsonIgnore] public ObservableCollection<string> GameMessages { get; } = [];
        public PopUpDetails PlayerDetails { get; set; }
        public PopUpDetails InventoryDetails { get; set; }
        public PopUpDetails QuestDetails { get; set; }
        public PopUpDetails RecipesDetails { get; set; }
        public PopUpDetails GameMessageDetails { get; set; }

        [JsonIgnore]
        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        [JsonIgnore]
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        [JsonIgnore]
        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        [JsonIgnore]
        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        [JsonIgnore] public World CurrentWorld { get; }
        [JsonIgnore] public bool HasMonster => _currentMonster != null;
        [JsonIgnore] public bool HasTrader => CurrentTrader != null;

        #endregion

        public GameSession(Player player, int xCoordinate, int yCoordinate)
        {
            PopulateGameDetails();

            CurrentWorld = WorldFactory.CreateWorld();
            CurrentPlayer = player;
            CurrentLocation = CurrentWorld.LocationAt(xCoordinate, yCoordinate);

            // SetUp popup window Properties
            PlayerDetails = new PopUpDetails
            {
                IsVisible = false,
                Top = 10,
                Left = 10,
                MinHeight = 75,
                MaxHeight = 400,
                MinWidth = 265,
                MaxWidth = 400
            };

            InventoryDetails = new PopUpDetails
            {
                IsVisible = false,
                Top = 500,
                Left = 10,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 250,
                MaxWidth = 400
            };

            QuestDetails = new PopUpDetails
            {
                IsVisible = false,
                Top = 500,
                Left = 275,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 250,
                MaxWidth = 400
            };

            RecipesDetails = new PopUpDetails
            {
                IsVisible = false,
                Top = 500,
                Left = 575,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 250,
                MaxWidth = 400
            };

            GameMessageDetails = new PopUpDetails
            {
                IsVisible = false,
                Top = 250,
                Left = 10,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 350,
                MaxWidth = 400
            };

            _messageBroker.OnMessageRaised += OnGameMessageRaised;
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate - 1,
                    CurrentLocation.YCoordinate);
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate + 1,
                    CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(
                    CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate - 1);
            }
        }

        private void PopulateGameDetails()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            if (GameMessages.Count > 250)
            {
                GameMessages.RemoveAt(0);
            }

            GameMessages.Add(e.Message);
        }

        private void CompleteQuestAtLocation()
        {
            foreach (var quest in CurrentLocation.QuestAvailableHere)
            {
                var questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsComplete);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemToComplete))
                    {
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemToComplete);

                        _messageBroker.RaiseMessage("");
                        _messageBroker.RaiseMessage($"You Completed the '{quest.Name}' quest.");

                        _messageBroker.RaiseMessage($"You Receive {quest.RewardExperiencePoints} Experience Points.");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);


                        _messageBroker.RaiseMessage($"You Receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);


                        foreach (var itemQuantity in quest.RewardItems)
                        {
                            var rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            _messageBroker.RaiseMessage($"You Receive {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }

                        questToComplete.IsComplete = true;
                    }
                }
            }
        }

        private void GivePlayerQuestAtLocation()
        {
            foreach (var quest in CurrentLocation.QuestAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage($"You receive the '{quest.Name}' quest.");
                    _messageBroker.RaiseMessage(quest.Description);

                    _messageBroker.RaiseMessage("Return with: ");
                    foreach (var itemQuantity in quest.ItemToComplete)
                    {
                        _messageBroker.RaiseMessage(
                            $"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                    _messageBroker.RaiseMessage("And You will receive: ");
                    _messageBroker.RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    _messageBroker.RaiseMessage($"{quest.RewardGold} gold");
                    foreach (var itemQuantity in quest.RewardItems)
                    {
                        _messageBroker.RaiseMessage(
                            $"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                }
            }
        }

        public void AttackCurrentMonster()
        {
            _currentBattle?.AttackOpponent();
        }

        public void UseCurrentConsumable()
        {
            if (CurrentPlayer?.CurrentConsumable != null)
            {
                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed += OnConsumableActionPerformed;
                }

                CurrentPlayer.UseCurrentConsumable();

                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed -= OnConsumableActionPerformed;
                }
            }
        }

        private void OnConsumableActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.Inventory.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);

                foreach (var itemQuantity in recipe.OutputItems)
                {
                    for (var i = 0; i < itemQuantity.Quantity; i++)
                    {
                        var outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem);
                        _messageBroker.RaiseMessage($"You Craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                _messageBroker.RaiseMessage("You do not have the required Ingredient!!!");
                foreach (var itemQuantity in recipe.Ingredients)
                {
                    _messageBroker.RaiseMessage($"{itemQuantity.QuantityItemDescription}");
                }
            }
        }

        private void OnPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"The Monster killed you.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            CurrentMonster = MonsterFactory.GetMonsterFromLocation(CurrentLocation);
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage($"You are now in Level {CurrentPlayer.Level}");
        }

        public void Dispose()
        {
            _currentBattle?.Dispose();
            _messageBroker.OnMessageRaised -= OnGameMessageRaised;
        }
    }
}
