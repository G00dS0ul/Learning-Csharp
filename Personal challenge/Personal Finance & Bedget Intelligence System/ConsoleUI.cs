using Personal_Finance___Bedget_Intelligence_System;
using static Personal_Finance___Bedget_Intelligence_System.Transaction;

namespace Personal_Finance___Budget_Intelligence_System
{
    public class ConsoleUI
    {
        private FinanceManager _manager;
        private bool _isRunning;


        public ConsoleUI()
        {
            _manager = new FinanceManager();
            _isRunning = true;

            Console.WriteLine("Initializing System...");
            _manager.LoadData();
            Console.WriteLine("Data Loaded. Press any Key to enter.");
            Console.ReadKey();
        }

        public void Run()
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (_isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n ====================================");
                Console.WriteLine("\n      FINANCE INTELLIGENT SYSTEM     ");
                Console.WriteLine("\n ====================================");
                Console.ResetColor();

                var culture = new System.Globalization.CultureInfo("en-NG");
                decimal balance = _manager.GetBalance();
                if (balance < 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Current Balance: {balance.ToString("C", culture)}(Low balance!!!)");
                    Console.ResetColor();
                }

                else
                {
                    Console.WriteLine($"Current Balance: {balance.ToString("C", culture)}");
                    Console.WriteLine("------------------------------------------");
                }

                    

                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. Edit Transaction");
                Console.WriteLine("3. Set Budget Rule");
                Console.WriteLine("4. View all Transaction");
                Console.WriteLine("5. Save & Exit");
                Console.Write("\nSelect an Option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddTransactionScreen();
                        break;
                    case "2":
                        EditTransactionScreen();
                        break;
                    case "3":
                        ShowBudgetRuleScreen();
                        break;
                    case "4":
                        ShowHistoryScreen();
                        break;
                    case "5":
                        _manager.SaveData();
                        _isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input!!! Press any Key.");
                        Console.ReadLine();
                        break;

                }

            }
        }

        private decimal GetValidDecimal(string prompt)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (decimal.TryParse(input, out value) && value >= 0)
                {
                    return value;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!!! Please enter a valid positive number.");
                Console.ResetColor();
            }
        }

        private void AddTransactionScreen()
        {
            Console.Clear();
            Console.WriteLine("\n--- NEW TRANSACTION ---");

            Console.Write("Enter Title: ");
            var title = Console.ReadLine();

            decimal amount = GetValidDecimal("Enter Amount: ");

            TransactionType type = GetTransactionType();

            Console.Write("Enter Category: ");
            var category = Console.ReadLine();

            

            _manager.AddTransaction(title, amount, DateTime.Now, type, category);

            var alert = _manager.CheckBudgetRules(category);
            if(!string.IsNullOrEmpty(alert))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(alert);
                Console.ResetColor();
                Console.Beep();
            }

            Console.WriteLine("Transaction Added!!! Press Any Key.");
            Console.ReadKey();

        }

        private void EditTransactionScreen()
        {
            Console.Clear();
            Console.WriteLine("\n---- Edit Transaction Details ----");

            Console.Write("Enter Transaction ID: ");
            var input = Console.ReadLine();
            
            if(int.TryParse(input, out int id))
            {
                Console.Write("Enter Ttile: ");
                var title = Console.ReadLine();

                decimal amount = GetValidDecimal("Enter Amount: ");

                TransactionType type = GetTransactionType();

                Console.Write("Enter Category: ");
                var category = Console.ReadLine();

                bool success = _manager.EditTransaction(id, title, amount, DateTime.Now, type, category);

                if(success)
                {
                    Console.WriteLine("Transaction Edited Successfully!!! Press Any Key.");
                }
                else
                {
                    Console.WriteLine("ID Not found!!!");
                }
            }

            else
            {
                Console.WriteLine("Invalid ID Format!!!");
            }

            Console.WriteLine("Press any key to Continue...");
            Console.ReadKey();
        }

        private TransactionType GetTransactionType()
        {
            while (true)
            {
                Console.WriteLine("\n ----Select Type----");
                Console.WriteLine("1. Income");
                Console.WriteLine("2. Expense");
                Console.Write("> ");

                var input = Console.ReadLine();

                if(int.TryParse(input, out int selection))
                {
                    if (selection == 1 || selection == 2)
                    {
                        return (TransactionType)selection;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid selection!!! Please type 1 or 2");
                Console.ResetColor();
            }
        }

        private void ShowBalanceScreen()
        {
            Console.Clear();
            Console.WriteLine("\n----Avalaible Balance----");
            Console.WriteLine($"NGN: {_manager.GetBalance()}");
            Console.WriteLine("Press any Key to continue.");
            Console.ReadKey();
        }

        private void ShowHistoryScreen()
        {
            Console.WriteLine("\n ----Transaction History----");
            ShowHistoryListOnly();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private void ShowHistoryListOnly()
        {
            var culture = new System.Globalization.CultureInfo("en-NG");

            var list = _manager.GetAllTransactions();

            if (list.Count == 0)
            {
                Console.WriteLine("No Transaction Found. ");
            }
            else
            {
                foreach (var item in list)
                {
                    Console.WriteLine($"[ID: {item.ID}] {item.Date.ToShortDateString()} - {item.Category}: {item.Amount.ToString("C", culture)} ({item.Type})");
                }
            }
        }

        private void ShowBudgetRuleScreen()
        {
            Console.Clear();
            var culture = new System.Globalization.CultureInfo("en-NG");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n ====================================");
            Console.WriteLine("\n      BUDGET INTELLIGENT SETUP       ");
            Console.WriteLine("\n ====================================");
            Console.ResetColor();

            Console.WriteLine("\n[ Active Rules ]");
            var currentRules = _manager.GetRules();

            if(currentRules.Count == 0)
            {
                Console.WriteLine("No Rules are Active");
            }
            else
            {
                foreach(var rule in currentRules)
                {
                    Console.WriteLine($"- {rule.Category}: Limit {rule.LimitAmount.ToString("C", culture)}");
                }
            }

            Console.WriteLine("---------------------------");

            Console.Write("\n Enter Category to Watch (e.g Food): ");
            var category = Console.ReadLine();

            decimal limit = GetValidDecimal($"Enter Monthly Limit for '{category}': ");

            _manager.AddBudgetRule(category, limit);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n SUCCESS: System will now alert you if '{category}' expenses exceed {limit.ToString("C", culture)}.");
            Console.ResetColor();

            Console.WriteLine("Press any key to Continue");
            Console.ReadKey();


        }
    }
}