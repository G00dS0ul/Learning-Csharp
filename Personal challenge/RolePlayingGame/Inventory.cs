namespace RolePlayingGame
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name} added to inventory");
        }

        public void RemoveItem(string itemName)
        {
            Item itemFound = items.FirstOrDefault(i => i.Name == itemName);
            if (itemFound != null)
            {
                items.Remove(itemFound);
                Console.WriteLine($"{itemName} removed from the inventory");
            }

            else
            {
                Console.WriteLine($"{itemName} not Found!");
            }

        }

        public void ShowInventory()
        {
            if(items.Count == 0)
            {
                Console.WriteLine("Inventory Is Empty!");
                return;
            }

            Console.WriteLine("Inventory: ");
            foreach (var item in items)
            {
                Console.WriteLine($"- {item}");
            }

        }

        public List<Item> ShowItemsByType(string type)
        {
            return items.Where(i => i.Type == type).ToList();
        }
    }
}