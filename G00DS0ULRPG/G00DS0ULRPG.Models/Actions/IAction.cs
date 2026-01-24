using G00DS0ULRPG.Models;

namespace G00DS0ULRPG.Models.Actions;

public interface IAction
{
    event EventHandler<string> OnActionPerformed;
    void Execute(LivingEntity actor, LivingEntity target);
}