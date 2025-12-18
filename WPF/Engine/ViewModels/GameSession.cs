using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs>? OnMessageRaised;

        #region Properties

        private Location? _currentLocation;
        private Monster? _currentMonster;
        private Trader? _currentTrader;
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                if (_currentLocation != value)
                {
                    _currentLocation = value;
                    OnPropertyChanged(nameof(CurrentLocation));
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
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                _currentMonster = value;

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null)
                {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here");
                }
            }
        }

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
            }
        }
       
        public Weapon? CurrentWeapon { get; set; }
        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        
        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        
        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
            
        
        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
            
        
        public World CurrentWorld { get; set; }

        public bool HasMonster => _currentMonster != null;

        public bool HasTrader => CurrentTrader != null;

        #endregion

        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Iche",
                CharacterClass = "Fighter",
                CurrentHitPoints = 10,
                MaximumHitPoints = 10,
                Gold = 100000,
                ExperiencePoints = 0,
                Level = 1
            };

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
            foreach(Quest quest in CurrentLocation.QuestAvailableHere)
            {
                QuestStatus questToComplete = CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsComplete);

                if(questToComplete != null)
                {
                    if(CurrentPlayer.HasAllTheseItems(quest.ItemToComplete))
                    {
                        foreach(ItemQuantity itemQuantity in quest.ItemToComplete)
                        {
                            for(int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You Completed the '{quest.Name}' quest.");

                        CurrentPlayer.ExperiencePoints += quest.RewardExperiencePoints;
                        RaiseMessage($"You Receive {quest.RewardExperiencePoints} Experience Points.");

                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You Receive {quest.RewardGold} gold");

                        foreach(ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            CurrentPlayer.AddItemToInventory(rewardItem);

                            RaiseMessage($"You Receive a {rewardItem.Name}");
                        }

                        questToComplete.IsComplete = true;
                    }
                }
            }
        }

        private void GivePlayerQuestAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest.");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with: ");
                    foreach(ItemQuantity itemQuantity in quest.ItemToComplete)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And You will receive: ");
                    RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"{quest.RewardGold} gold");
                    foreach(ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
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
            if (CurrentWeapon == null)
            {
                RaiseMessage("You Must Select a Weapon, to Attack!!!");
                return;
            }

            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}");
            }
            else
            {
                CurrentMonster.CurrentHitPoints -= damageToMonster;
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} points.");
            }

            if(CurrentMonster.CurrentHitPoints <= 0)
            {
                RaiseMessage("");
                RaiseMessage($"You defeated the {CurrentMonster.Name}!!!");

                CurrentPlayer.ExperiencePoints += CurrentMonster.RewardExperiencePoints;
                RaiseMessage($"You Receive {CurrentMonster.RewardExperiencePoints} experience Points.");

                CurrentPlayer.Gold += CurrentMonster.Gold;
                RaiseMessage($"You Receive {CurrentMonster.Gold} gold.");

                foreach (GameItem gameItem in CurrentMonster.Inventory)
                {
                    CurrentPlayer.AddItemToInventory(gameItem);
                    RaiseMessage($"You Receive one {gameItem.Name}.");
                }

                GetMonsterAtLocation();
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinimumDamage, CurrentMonster.MaximumDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you");
                }
                else
                {
                    CurrentPlayer.CurrentHitPoints -= damageToPlayer;
                    RaiseMessage($"The {CurrentMonster.Name} hits you for {damageToPlayer} Points. ");
                }

                if(CurrentPlayer.CurrentHitPoints <= 0)
                {
                    RaiseMessage("");
                    RaiseMessage($"The {CurrentMonster.Name} killed you.");

                    CurrentLocation = CurrentWorld.LocationAt(0, -1);
                    CurrentPlayer.CurrentHitPoints = CurrentPlayer.Level * 10;
                }
            }
        }

        
        
        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
