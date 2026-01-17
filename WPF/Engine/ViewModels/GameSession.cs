using Engine.Models;
using Engine.Factories;
using Engine.Services;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();

        private Battle _currentBattle;

        #region Properties

        private Player? _currentPlayer;
        private Location? _currentLocation;
        private Monster? _currentMonster;
        private Trader? _currentTrader;

        public Player CurrentPlayer 
        {

            get
            {
                return _currentPlayer;
            }
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnPlayerKilled;
                }

                _currentPlayer = value;

                if(_currentPlayer != null)
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

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToSouth));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToEast));

                CompleteQuestAtLocation();
                GivePlayerQuestAtLocation();
                CurrentMonster = CurrentLocation.GetMonster();

                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(CurrentPlayer, CurrentMonster);

                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;

                    _currentMonster = value;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }
       
        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        
        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        
        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
            
        
        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
            
        
        public World CurrentWorld { get; }

        public bool HasMonster => _currentMonster != null;

        public bool HasTrader => CurrentTrader != null;

        #endregion

        public GameSession()
        {
            int dexterity = RandomNumberGenerator.NumberBetween(3, 18);

            CurrentPlayer = new Player("G00dS0ul", "Fighter", 0, 10, 10, dexterity, 1000000);
          

            if(!CurrentPlayer.Inventory.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            CurrentPlayer.LearnRecipe(RecipeFactory.GetRecipeByID(
                1));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(3003));
            
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0);

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

        private void CompleteQuestAtLocation()
        {
            foreach(var quest in CurrentLocation.QuestAvailableHere)
            {
                var questToComplete = CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsComplete);

                if(questToComplete != null)
                {
                    if(CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemToComplete))
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
                            for (var i = 0; i < itemQuantity.Quantity; i++)
                            {
                                var rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                                CurrentPlayer.AddItemToInventory(rewardItem);
                            }
                            _messageBroker.RaiseMessage($"You Receive {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
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
                    foreach(var itemQuantity in quest.ItemToComplete)
                    {
                        _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                    _messageBroker.RaiseMessage("And You will receive: ");
                    _messageBroker.RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    _messageBroker.RaiseMessage($"{quest.RewardGold} gold");
                    foreach(var itemQuantity in quest.RewardItems)
                    {
                        _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                }
            }
        }

        public void AttackCurrentMonster()
        {
           _currentBattle.AttackOpponent();
        }

        public void UseCurrentConsumable()
        {
            if (CurrentPlayer?.CurrentConsumable != null)
            {
                CurrentPlayer.UseCurrentConsumable();
            }
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
                    _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
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
            CurrentMonster = CurrentLocation.GetMonster();
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage($"You are now in Level {CurrentPlayer.Level}");
        }

        

    }
}
