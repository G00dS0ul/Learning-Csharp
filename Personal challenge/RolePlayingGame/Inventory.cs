namespace RolePlayingGame
{
    public class Inventory
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            ConsoleUI.PrintColor($"{item.Name} added to inventory", ConsoleColor.Green);
        }

        public void RemoveItem(Item itemFound)
        {
            if (itemFound != null)
            {
                items.Remove(itemFound);
                Console.WriteLine($"{itemFound} removed from the inventory");
            }

            else
            {
                Console.WriteLine($"{itemFound} not Found!");
            }

        }

        public void ShowInventory()
        {
            if(items.Count == 0)
            {
                ConsoleUI.PrintColor("Inventory Is Empty!", ConsoleColor.Red);
                return;
            }

            ConsoleUI.PrintColor("Inventory: ", ConsoleColor.Cyan);
            for (int i = 0; i < items.Count; i++)
            {
                ConsoleUI.PrintColor($"{1 + i}. {items[i].Name} - {items[i].Value}.", ConsoleColor.Magenta);
            }

        }

        public List<Item> ShowItemsByType(string type)
        {
            return items.Where(i => i.Type == type).ToList();
        }
    }
}