namespace RolePlayingGame
{
    public class Inventory
    {
        public List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name} added to inventory");
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
                Console.WriteLine("Inventory Is Empty!");
                return;
            }

            Console.WriteLine("Inventory: ");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{1 + i}. {items[i].Name} - {items[i].Value}.");
            }

        }

        public List<Item> ShowItemsByType(string type)
        {
            return items.Where(i => i.Type == type).ToList();
        }
    }
}