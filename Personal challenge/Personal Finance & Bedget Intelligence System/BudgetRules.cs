namespace Personal_Finance___Budget_Intelligence_System
{
    public class BudgetRules
    {
        public string Category { get; set; }
        public decimal LimitAmount { get; set; }
        public string WarningMessage { get; set; }

        public BudgetRules(string category, decimal limitAmount, string warningMessage)
        {
            this.Category = category;
            this.LimitAmount = limitAmount;
            this.WarningMessage = warningMessage;
        }
    }
}