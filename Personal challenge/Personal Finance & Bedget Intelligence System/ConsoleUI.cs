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

            ConsoleForegroundColor.PrintColor("Initializing System...", ConsoleColor.Red);
            _manager.LoadData();
            ConsoleForegroundColor.PrintColor("Data Loaded. Press any Key to enter.", ConsoleColor.Green);
            Console.ReadKey();
        }

        public void Run()
        {
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (_isRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                ConsoleForegroundColor.PrintColor("\n ====================================", ConsoleColor.DarkCyan);
                ConsoleForegroundColor.PrintColor("\n      FINANCE INTELLIGENT SYSTEM     ", ConsoleColor.DarkCyan);
                ConsoleForegroundColor.PrintColor("\n ====================================", ConsoleColor.DarkCyan);
                Console.ResetColor();

                var culture = new System.Globalization.CultureInfo("en-NG");
                decimal balance = _manager.GetBalance();
                if (balance < 1000)
                {
                    ConsoleForegroundColor.PrintColor($"Current Balance: {balance.ToString("C", culture)}(Low balance!!!)", ConsoleColor.Red);
                }

                else
                {
                    ConsoleForegroundColor.PrintColor($"Current Balance: {balance.ToString("C", culture)}", ConsoleColor.Green);
                    ConsoleForegroundColor.PrintColor("------------------------------------------", ConsoleColor.DarkBlue);
                }

                    

                ConsoleForegroundColor.PrintColor("1. Add Transaction", ConsoleColor.Green);
                ConsoleForegroundColor.PrintColor("2. Edit Transaction", ConsoleColor.DarkGray);
                ConsoleForegroundColor.PrintColor("3. Set Budget Rule", ConsoleColor.Magenta);
                ConsoleForegroundColor.PrintColor("4. View all Transaction", ConsoleColor.Blue);
                ConsoleForegroundColor.PrintColor("5. Reset All Data", ConsoleColor.Red);
                ConsoleForegroundColor.PrintColor("6. Save & Exit", ConsoleColor.DarkYellow);
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
                        ResetDataScreen();
                        break;
                    case "6":
                        _manager.SaveData();
                        _isRunning = false;
                        break;
                    default:
                        ConsoleForegroundColor.PrintColor("Invalid Input!!! Press any Key.", ConsoleColor.Red);
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

                ConsoleForegroundColor.PrintColor("Invalid input!!! Please enter a valid positive number.", ConsoleColor.Red);
            }
        }

        private void AddTransactionScreen()
        {
            Console.Clear();
            ConsoleForegroundColor.PrintColor("\n--- NEW TRANSACTION ---", ConsoleColor.DarkCyan);

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
                ConsoleForegroundColor.PrintColor(alert, ConsoleColor.Red);
                Console.Beep();
            }

            ConsoleForegroundColor.PrintColor("Transaction Added!!! Press Any Key.", ConsoleColor.DarkYellow);
            Console.ReadKey();

        }

        private void EditTransactionScreen()
        {
            Console.Clear();
            ConsoleForegroundColor.PrintColor("\n---- Edit Transaction Details ----", ConsoleColor.DarkCyan);

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
                    ConsoleForegroundColor.PrintColor("Transaction Edited Successfully!!! Press Any Key.", ConsoleColor.Green);
                }
                else
                {
                    ConsoleForegroundColor.PrintColor("ID Not found!!!", ConsoleColor.Red);
                }
            }

            else
            {
                ConsoleForegroundColor.PrintColor("Invalid ID Format!!!", ConsoleColor.Red);
            }

            ConsoleForegroundColor.PrintColor("Press any key to Continue...", ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        private TransactionType GetTransactionType()
        {
            while (true)
            {
                ConsoleForegroundColor.PrintColor("\n ----Select Type----", ConsoleColor.DarkCyan);
                ConsoleForegroundColor.PrintColor("1. Income", ConsoleColor.DarkGreen);
                ConsoleForegroundColor.PrintColor("2. Expense", ConsoleColor.DarkRed);
                Console.Write("> ");

                var input = Console.ReadLine();

                if(int.TryParse(input, out int selection))
                {
                    if (selection == 1 || selection == 2)
                    {
                        return (TransactionType)selection;
                    }
                }

                ConsoleForegroundColor.PrintColor("Invalid selection!!! Please type 1 or 2", ConsoleColor.DarkRed);
            }
        }

        private void ShowBalanceScreen()
        {
            Console.Clear();
            ConsoleForegroundColor.PrintColor("\n----Avalaible Balance----", ConsoleColor.DarkCyan);
            ConsoleForegroundColor.PrintColor($"NGN: {_manager.GetBalance()}", ConsoleColor.Green);
            ConsoleForegroundColor.PrintColor("Press any Key to continue.", ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        private void ShowHistoryScreen()
        {
            ConsoleForegroundColor.PrintColor("\n ----Transaction History----", ConsoleColor.DarkCyan);
            ShowHistoryListOnly();
            ConsoleForegroundColor.PrintColor("Press any key to continue.", ConsoleColor.DarkYellow);
            Console.ReadKey();
        }

        private void ShowHistoryListOnly()
        {
            var culture = new System.Globalization.CultureInfo("en-NG");

            var list = _manager.GetAllTransactions();

            if (list.Count == 0)
            {
                ConsoleForegroundColor.PrintColor("No Transaction Found. ", ConsoleColor.Red);
            }
            else
            {
                foreach (var item in list)
                {
                    ConsoleForegroundColor.PrintColor($"[ID: {item.ID}] {item.Date.ToShortDateString()} - {item.Category}: {item.Amount.ToString("C", culture)} ({item.Type})", ConsoleColor.Yellow);
                }
            }
        }

        private void ShowBudgetRuleScreen()
        {
            Console.Clear();
            var culture = new System.Globalization.CultureInfo("en-NG");
            ConsoleForegroundColor.PrintColor("\n ====================================", ConsoleColor.Yellow);
            ConsoleForegroundColor.PrintColor("\n      BUDGET INTELLIGENT SETUP       ", ConsoleColor.Yellow);
            ConsoleForegroundColor.PrintColor("\n ====================================", ConsoleColor.Yellow);

            ConsoleForegroundColor.PrintColor("\n[ Active Rules ]", ConsoleColor.DarkCyan);
            var currentRules = _manager.GetRules();

            if(currentRules.Count == 0)
            {
                ConsoleForegroundColor.PrintColor("No Rules are Active", ConsoleColor.Red);
            }
            else
            {
                foreach(var rule in currentRules)
                {
                    ConsoleForegroundColor.PrintColor($"- {rule.Category}: Limit {rule.LimitAmount.ToString("C", culture)}", ConsoleColor.Magenta);
                }
            }

            ConsoleForegroundColor.PrintColor("---------------------------", ConsoleColor.DarkBlue);

            Console.Write("\n Enter Category to Watch (e.g Food): ");
            var category = Console.ReadLine();

            decimal limit = GetValidDecimal($"Enter Monthly Limit for '{category}': ");

            _manager.AddBudgetRule(category, limit);

            ConsoleForegroundColor.PrintColor($"\n SUCCESS: System will now alert you if '{category}' expenses exceed {limit.ToString("C", culture)}.", ConsoleColor.Green);

            ConsoleForegroundColor.PrintColor("Press any key to Continue", ConsoleColor.DarkYellow);
            Console.ReadKey();


        }

        private void ResetDataScreen()
        {
            Console.Clear();
            ConsoleForegroundColor.PrintColor("\n ----WARNING: This will delete ALL your data!", ConsoleColor.Red);
            ConsoleForegroundColor.PrintColor("This Action cannot be undone.", ConsoleColor.Red);

            Console.Write("\nType 'CONFIRM' to reset all data: ");
            var confirmation = Console.ReadLine();

            if(confirmation?.ToUpper() == "CONFIRM")
            {
                try
                {
                    _manager.ResetAllData();
                    
                    ConsoleForegroundColor.PrintColor("Data reset Successfully!!!", ConsoleColor.Green);
                    Console.Beep();
                    ConsoleForegroundColor.PrintColor("Please restart the application to start fresh.", ConsoleColor.DarkYellow);
                }
                   
                catch (Exception ex)
                {
                    ConsoleForegroundColor.PrintColor($"Error resetting data: {ex.Message}", ConsoleColor.DarkRed);
                }
            }
            else
            {
                ConsoleForegroundColor.PrintColor("Reset Cancelled", ConsoleColor.Red);
            }

            ConsoleForegroundColor.PrintColor("Press any key to continue...", ConsoleColor.DarkYellow);
            Console.ReadKey();
        }
    }
}