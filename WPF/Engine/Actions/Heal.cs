using System;
using Engine.Actions;
using Engine.Models;

namespace Engine.Models
{
    public class Heal : IAction
    {
        private readonly GameItem _item;
        private readonly int _hitPointsToHeal;

        public event EventHandler<string> OnActionPerformed;

        public Heal(GameItem item, int hitPointsToHeal)
        {
            if (item.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{item.Name} is not Consumable, Cannot Heal!!!");
            }

            _item = item;
            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            var actorName = (actor is Player) ? "You" : $"The {actor.Name?.ToLower()}";
            var targetName = (target is Player) ? "yourself" : $"the {target.Name?.ToLower()}";

            ReportResult(
                $"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal > 1 ? "s" : "")}.");
            target.Heal(_hitPointsToHeal);

        }

        private void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
