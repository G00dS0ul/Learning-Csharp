namespace Personal_Finance___Bedget_Intelligence_System
{
    public class Transaction
    {
        public enum TransactionType
        {
            Income = 1,
            Expense = 2
        }

        public int ID { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

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

       public override string ToString()
        {
            var culture = new System.Globalization.CultureInfo("en-NG");
            return $"ID: {ID}, Type: {Type}, Amount: {Amount.ToString("C", culture)}, Category: {Category}, Date: {Date:yyyy-MM-dd}, Description: {Description}";
        }
    }
}