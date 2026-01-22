using Engine.Models;
using Engine.Shared;

namespace Engine.Services
{
    public class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

        public static Combatant FirstAttacker(Player player, Monster opponent)
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

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            var playerDexterity = attacker.GetAttribute("DEX").ModifiedValue * attacker.GetAttribute("DEX").ModifiedValue;
            var opponentDexterity = target.GetAttribute("DEX").ModifiedValue * target.GetAttribute("DEX").ModifiedValue;
            var dexterityOffset = (
                playerDexterity - opponentDexterity) / 10m;
            var randomOffset = DiceService.Instance.Roll(20).Value - 10;
            var totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset;
        }
    }
}
