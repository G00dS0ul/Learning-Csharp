using Engine.Models;

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
            var playerDexterity = player.Dexterity * player.Dexterity;
            var opponentDexterity = opponent.Dexterity * opponent.Dexterity;
            var dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            var randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            var totalOffset = dexterityOffset + randomOffset;

            return RandomNumberGenerator.NumberBetween(0, 100) <= 50 + totalOffset
                ? Combatant.Player
                : Combatant.Opponent;

        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            var playerDexterity = attacker.Dexterity * attacker.Dexterity;
            var opponentDexterity = target.Dexterity * target.Dexterity;
            var dexterityOffset = (
                playerDexterity - opponentDexterity) / 10m;
            var randomOffset = RandomNumberGenerator.NumberBetween(-10, 10);
            var totalOffset = dexterityOffset + randomOffset;

            return RandomNumberGenerator.NumberBetween(0, 100) <= 50 + totalOffset;
        }
    }
}
