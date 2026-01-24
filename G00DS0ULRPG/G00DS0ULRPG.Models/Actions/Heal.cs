using G00DS0ULRPG.Models;

namespace G00DS0ULRPG.Models.Actions
{
    public class Heal : BaseAction, IAction
    {
        private readonly GameItem _item;
        private readonly int _hitPointsToHeal;

        //public event EventHandler<string> OnActionPerformed;

        public Heal(GameItem itemInUse, int hitPointsToHeal)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not Consumable, Cannot Heal!!!");
            }

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
    }
}
