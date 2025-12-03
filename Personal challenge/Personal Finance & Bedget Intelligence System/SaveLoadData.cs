using Personal_Finance___Bedget_Intelligence_System;
using Personal_Finance___Budget_Intelligence_System;
using System.Text.Json;
using static Personal_Finance___Bedget_Intelligence_System.Transaction;

namespace Personal_Finance___Budget_Intelligence_System
{
    public class SaveLoadData
    {
        private const string SaveFolder = "Saves";

        public List<Transaction> Transactions { get; set; }
        public List<BudgetRules> Rules { get; set; }
        public int LastID { get; set; }


        public void SaveSystem(string fileName, List<Transaction> currentTransactions, List<BudgetRules> currentRules, int currentId)
        {
            if(!Directory.Exists(SaveFolder))
                Directory.CreateDirectory(SaveFolder);

            var path = Path.Combine(SaveFolder, fileName + ".json");

            this.Transactions = currentTransactions;
            this.Rules = currentRules;
            this.LastID = currentId;

            try
            {
                var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
                ConsoleForegroundColor.PrintColor($"Saved Succefully to: {fileName}", ConsoleColor.Green);
            }

            catch (Exception ex)
            {
                ConsoleForegroundColor.PrintColor($"Error Saving!!! {ex.Message}", ConsoleColor.DarkRed);
            }
        }

        public SaveLoadData LoadSystem(string fileName)
        {
            var path = Path.Combine(SaveFolder, fileName + ".json");

            if (!File.Exists(path))
                return null;

            try
            {
                var json = File.ReadAllText(path);
                SaveLoadData data = JsonSerializer.Deserialize<SaveLoadData>(json);
                ConsoleForegroundColor.PrintColor("Loaded Data Successfully", ConsoleColor.Green);
                return data;

                
            }

            catch(Exception ex)
            {
                ConsoleForegroundColor.PrintColor($"Error Loading Data!!! {ex.Message}", ConsoleColor.Red);
                return null;
            }
            
        }
    }
}