using System.ComponentModel;
using Engine.Services;

namespace Engine.Models
{
    public class PlayerAttribute : INotifyPropertyChanged
    {
        private int _modifiedValue;
        public string? Key { get; }
        public string DisplayName { get; }
        public string DiceNotation { get; }
        public int BaseValue { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int ModifiedValue
        {
            get => _modifiedValue;
            set
            {
                _modifiedValue = value;
            }
        }


        public PlayerAttribute(string key, string displayName, string diceNotation) 
            : this(key, displayName, diceNotation, DiceService.Instance.Roll(diceNotation).Value)
        {
        }

        public PlayerAttribute(string key, string displayName, string diceNotation, int baseValue) 
            : this(key, displayName, diceNotation, baseValue, baseValue)
        {
        }

        public PlayerAttribute(string key, string displayName, string diceNotation, int baseValue, int modifiedValue)
        {
            Key = key;
            DisplayName = displayName;
            DiceNotation = diceNotation;
            BaseValue = baseValue;
            ModifiedValue = baseValue;
        }

        public void ReRoll()
        {
            BaseValue = DiceService.Instance.Roll(DiceNotation).Value;
            ModifiedValue = BaseValue;
        }
    }
}
