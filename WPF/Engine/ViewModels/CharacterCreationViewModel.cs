using System.Collections.ObjectModel;
using Engine.Models;
using Engine.Services;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseNotificationClass
    {
        private Race _selectedRace;
        public GameDetails GameDetails { get; }

        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } = [];

        public bool HasRaces => GameDetails.Races.Any();

        public bool HasRaseAttributeModifiers =>
            HasRaces && GameDetails.Races.Any(r => r.PlayerAttributeModifiers.Any());

        public CharacterCreationViewModel()
        {
            GameDetails = GameDetailsService.ReadGameDetails();

            if (HasRaces)
            {
                SelectedRace = GameDetails.Races.First();
            }

            RollNewCharacter();
        }

        public void RollNewCharacter()
        {
            PlayerAttributes.Clear();

            foreach (var playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.ReRoll();
                PlayerAttributes.Add(playerAttribute);
            }

            ApplyAttributeModifiers();
        }

        public void ApplyAttributeModifiers()
        {
            foreach (var playerAttribute in PlayerAttributes)
            {
                var attributeRaceModifier =
                    SelectedRace.PlayerAttributeModifiers.FirstOrDefault(pam =>
                        pam.AttributeKey.Equals(playerAttribute.Key));
                playerAttribute.ModifiedValue = playerAttribute.BaseValue + (attributeRaceModifier?.Modifier ?? 0);
            }
        }

        public Player GetPlayer()
        {
            var player = new Player(Name, 0, 10, 10, PlayerAttributes, 10);

            player.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3003));

            return player;
        }
    }
}
