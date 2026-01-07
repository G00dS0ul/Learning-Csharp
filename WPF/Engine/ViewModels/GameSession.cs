using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs>? OnMessageRaised;

        #region Properties

        private Player _currentPlayer;
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
                    _currentPlayer.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }

                _currentPlayer = value;

                if(_currentPlayer != null)
                {
                    _currentPlayer.OnActionPerformed += OnCurrentPlayerPerformedAction; _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
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
                GetMonsterAtLocation();

                    CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed -= OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed += OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!!!");
                }

                _currentMonster = value;

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
            CurrentPlayer = new Player("G00dS0ul", "Fighter", 0, 10, 10, 1000000);
          

            if(!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            }

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
                    if(CurrentPlayer.HasAllTheseItems(quest.ItemToComplete))
                    {
                        foreach(var itemQuantity in quest.ItemToComplete)
                        {
                            for(var i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You Completed the '{quest.Name}' quest.");

                        RaiseMessage($"You Receive {quest.RewardExperiencePoints} Experience Points.");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);


                        RaiseMessage($"You Receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);


                        foreach (var itemQuantity in quest.RewardItems)
                        {
                            var rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);


                            RaiseMessage($"You Receive a {rewardItem.Name}");
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

                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest.");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with: ");
                    foreach(var itemQuantity in quest.ItemToComplete)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                    RaiseMessage("And You will receive: ");
                    RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"{quest.RewardGold} gold");
                    foreach(var itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID)?.Name}");
                    }

                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void AttackCurrentMonster()
        {
            if (CurrentPlayer?.CurrentWeapon == null)
            {
                RaiseMessage("You Must Select a Weapon, to Attack!!!");
                return;
            }

            CurrentPlayer.UseCurrentWeapon(CurrentMonster);

            if(CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }

            else
            {
                CurrentMonster.UseCurrentWeapon(CurrentPlayer);

            }
        }

        private void OnCurrentPlayerPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentMonsterPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"The Monster killed you.");

            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!!!");

            RaiseMessage($"You Receive {CurrentMonster.RewardExperiencePoints} experience Points.");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);


            RaiseMessage($"You Receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);

            foreach (var gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You Receive one {gameItem.Name}.");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"You are now in Level {CurrentPlayer.Level}");
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

    }
}
