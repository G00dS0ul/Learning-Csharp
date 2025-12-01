namespace Personal_Finance___Bedget_Intelligence_System
{
    public class Transaction
    {
        public enum TransactionType
        {
            Income = 1,
            Expense = 2
        }

        public int ID;
        public TransactionType Type;
        public decimal Amount;
        public string Category;
        public DateTime Date;
        public string Description;

        public Transaction()
        {
            
        }

        public Transaction(int iD, TransactionType type, decimal amount, string category, DateTime date, string description)
        {
            this.ID = iD;
            this.Type = type;
            this.Amount = amount;
            this.Category = category;
            this.Date = date;
            this.Description = description;
        }

       public virtual void ToString()
        {

        }
    }
}