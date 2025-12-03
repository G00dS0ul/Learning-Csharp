using Personal_Finance___Budget_Intelligence_System;
using static Personal_Finance___Bedget_Intelligence_System.Transaction;

namespace Personal_Finance___Bedget_Intelligence_System
{
    public class FinanceManager
    {
        private Dictionary<int, Transaction> _transaction;
        private List<BudgetRules> _rule = new List<BudgetRules>();
        private int nextID;

        public FinanceManager()
        {
            _transaction = new Dictionary<int, Transaction>();
            _rule = new List<BudgetRules>();
            nextID = 1;

        }

        public int AddTransaction(string title, decimal amount, DateTime date, TransactionType type, string category)
        {
            int id = nextID;
            Transaction newTransaction = new Transaction(id, type, amount, category, date, title);
            _transaction.Add(id, newTransaction);
            nextID++;
            return id;
            
        }

        public bool RemoveTransaction(int id)
        {
            if (_transaction.ContainsKey(id))
            {
                _transaction.Remove(id);
                return true;
            }
            return false;
        }

        public bool EditTransaction(int id, string title, decimal amount, DateTime date, TransactionType type, string category)
        {
            if (_transaction.ContainsKey(id))
            {
                var transactionToEdit = _transaction[id];

                transactionToEdit.Description = title;
                transactionToEdit.Category = category;
                transactionToEdit.Amount = amount;
                transactionToEdit.Date = date;
                transactionToEdit.Type = type;

                return true;
            }



            return false;
        }

        public decimal GetTotalIncome()
        {
            return _transaction.Values
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);
        }

        public decimal GetTotalExpense()
        {
            return _transaction.Values
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);
        }

        public decimal GetBalance()
        {
            return GetTotalIncome() - GetTotalExpense();
        }

        public List<Transaction> GetTransactionByDate(DateTime startDate, DateTime endDate)
        {
            return _transaction.Values
                .Where(t => t.Date >= startDate && t.Date <=  endDate)
                .ToList();
        }

        public List<Transaction> GetTransactionByCategory(string category)
        {
            return _transaction.Values
                .Where(t => t.Category == category)
                .ToList();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transaction.Values
                .ToList();
        }

        public void PrintMonthlySummary()
        {
            var summary = _transaction.Values
                .GroupBy(t => t.Date.ToString("yyyy-MM"))
                .Select(group => new
                {
                    Month = group.Key,
                    TotalIncome = group.Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount),
                    TotalExpense = group.Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount)
                })
                .OrderByDescending(x => x.Month);

            foreach (var item in summary)
            {
                ConsoleForegroundColor.PrintColor($"Month: {item.Month} | Income: {item.TotalIncome} | Expense: {item.TotalExpense}", ConsoleColor.DarkMagenta);
            }
                
        }

        public void AddBudgetRule(string category, decimal limit)
        {
            var culture = new System.Globalization.CultureInfo("en-NG");
            var msg = $"WARNING: You have exceeded your {category} budget  of {limit.ToString("C", culture)}!!";
            _rule.Add(new BudgetRules(category, limit, msg));
        }

        public string CheckBudgetRules(string category)
        {
            var rule = _rule.FirstOrDefault(r => r.Category != null && r.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (rule != null)
            {
                decimal totalSpent = _transaction.Values
                    .Where(t => t.Category != null && t.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                if (totalSpent > rule.LimitAmount)
                {
                    return rule.WarningMessage;
                }
            }

            return null;
        }

        public List<BudgetRules> GetRules()
        {
            return _rule;
        }
        public void SaveData()
        {
            SaveLoadData saver = new SaveLoadData();

            List<Transaction> transList = _transaction.Values.ToList();

            saver.SaveSystem("my_finance_save", transList, _rule, nextID);
        }

        public void LoadData()
        {
            var loader = new SaveLoadData();
            SaveLoadData loadedData = loader.LoadSystem("my_finance_save");

            if( loadedData != null )
            {
                nextID = loadedData.LastID;
                _rule = loadedData.Rules;
                LoadFromList(loadedData.Transactions);

                ConsoleForegroundColor.PrintColor("System State Restored!", ConsoleColor.Green);
            }
        }

        public void ResetAllData()
        {
            _transaction.Clear();
            _rule.Clear();
            nextID = 1;

            try
            {
                var savePath = Path.Combine("Saves", "my_finance_save.json");
                if(File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
            }

            catch(Exception ex)
            {
                ConsoleForegroundColor.PrintColor($"Error deleting save file: {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        public void LoadFromList(List<Transaction> list)
        {
            _transaction.Clear();
            foreach (var items in list)
            {
                if(items.ID <= 0 || string.IsNullOrEmpty(items.Description))
                {
                    ConsoleForegroundColor.PrintColor($"WAENING: Skipping Invalid transaction with ID: {items.ID}", ConsoleColor.Red);
                    continue;
                }
                if (!_transaction.ContainsKey(items.ID))
                {
                    _transaction.Add(items.ID, items);
                }
                else
                {
                    ConsoleForegroundColor.PrintColor($"WARNING: Duplicate {items.ID}", ConsoleColor.Red);
                }
            }

            if(list.Count > 0)
            {
                nextID = list.Max(t => t.ID) + 1;
            }
        }
    }
}