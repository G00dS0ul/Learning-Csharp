using G00DS0ULRPG.Models;

namespace G00DS0ULRPG.Models.Actions
{
    public abstract class BaseAction
    {
        protected readonly GameItem _itemInUse;
        public event EventHandler<string> OnActionPerformed;

        protected BaseAction(GameItem itemInUse)
        {
            _itemInUse = itemInUse;
        }

        protected void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
