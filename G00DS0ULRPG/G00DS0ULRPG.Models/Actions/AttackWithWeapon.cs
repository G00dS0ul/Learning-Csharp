using G00DS0ULRPG.Models;
using G00DS0ULRPG.Models.Shared;
using G00DS0ULRPG.Core;

namespace G00DS0ULRPG.Models.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {
        private readonly string _damageDice;

        public AttackWithWeapon(GameItem itemInUse, string damageDice)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a weapon");
            }

            if (string.IsNullOrWhiteSpace(damageDice))
            {
                throw new ArgumentException("damageDice must be a valid dice notation");
            }

            // Parse and validate dice notation
            ValidateDiceNotation(damageDice);

            _damageDice = damageDice;
        }

        private void ValidateDiceNotation(string damageDice)
        {
            // Parse notation like "5d3" to extract minimum and maximum damage
            var parts = damageDice.Split('d', 'D');
            if (parts.Length != 2)
            {
                throw new ArgumentException("damageDice must be in valid dice notation (e.g., '1d5')");
            }

            if (!int.TryParse(parts[0], out var minimumDamage) || !int.TryParse(parts[1], out var maximumDamage))
            {
                throw new ArgumentException("damageDice must contain valid integers");
            }

            if (maximumDamage < minimumDamage)
            {
                throw new ArgumentException("Maximum damage cannot be less than minimum damage");
            }
        }

        private static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            var playerDexterity = attacker.GetAttribute("DEX").ModifiedValue * attacker.GetAttribute("DEX").ModifiedValue;
            var opponentDexterity = target.GetAttribute("DEX").ModifiedValue * target.GetAttribute("DEX").ModifiedValue;
            var dexterityOffset = (
                playerDexterity - opponentDexterity) / 10m;
            var randomOffset = DiceService.Instance.Roll(20).Value - 10;
            var totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            var actorName = (actor is Player) ? "You" : $"The {actor.Name?.ToLower()}";
            var targetName = (target is Player) ? "you" : $"the {target.Name?.ToLower()}";

            if (AttackSucceeded(actor, target))
            {
                var damage = DiceService.Instance.Roll(_damageDice).Value;
                ReportResult($"{actorName} hits {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");

                target.TakeDamage(damage);
            }
            else
            {
                ReportResult($"{actorName} missed {targetName}.");
            }
            
        }

        
    }
}
