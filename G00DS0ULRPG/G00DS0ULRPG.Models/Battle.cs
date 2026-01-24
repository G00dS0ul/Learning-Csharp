using G00DS0ULRPG.Models.Shared;
using G00DS0ULRPG.Core;
using G00DS0ULRPG.Models.EventArgs;

namespace G00DS0ULRPG.Models
{
    public class Battle : IDisposable
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private readonly Player _player;
        private readonly Monster _opponent;

        private enum Combatant
        {
            Player,
            Opponent
        }


        public event EventHandler<CombatVictoryEventArgs> OnCombatVictory;

        public Battle(Player player, Monster opponent)
        {
            _player = player;
            _opponent = opponent;

            _player.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnKilled += OnOpponentKilled;

            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You see a {_opponent.Name} appears!");

            if (FirstAttacker(_player, _opponent) == Combatant.Opponent)
            {
                AttackPlayer();
            }

        }

        public void AttackOpponent()
        {
            if (_player?.CurrentWeapon == null)
            {
                _messageBroker.RaiseMessage("You must select a weapon to Attack!!");
                return;
            }

            _player.UseCurrentWeapon(_opponent);

            if (_opponent.IsAlive)
            {
                AttackPlayer();
            }
        }

        public void Dispose()
        {
            _player.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnKilled -= OnOpponentKilled;
        }

        private void OnOpponentKilled(object sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You defeated the {_opponent.Name}!");

            _messageBroker.RaiseMessage($"You receive {_opponent.RewardExperiencePoints} experience points.");
            _player.AddExperience(_opponent.RewardExperiencePoints);

            _messageBroker.RaiseMessage($"You receive {_opponent.Gold} gold.");
            _player.ReceiveGold(_opponent.Gold);

            foreach (var gameItem in _opponent.Inventory.Items)
            {
                _messageBroker.RaiseMessage($"You receive one {gameItem.Name}.");
                _player.AddItemToInventory(gameItem);

            }

            OnCombatVictory?.Invoke(this, new CombatVictoryEventArgs());

        }

        private void AttackPlayer()
        {
            _opponent.UseCurrentWeapon((_player));
        }

        private void OnCombatantActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }

        private static Combatant FirstAttacker(Player player, Monster opponent)
        {
            var playerDexterity = player.GetAttribute("DEX").ModifiedValue * player.GetAttribute("DEX").ModifiedValue;
            var opponentDexterity = opponent.GetAttribute("DEX").ModifiedValue * opponent.GetAttribute("DEX").ModifiedValue;
            var dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            var randomOffset = DiceService.Instance.Roll(20).Value - 10;
            var totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset
                ? Combatant.Player
                : Combatant.Opponent;

        }

    }
}
