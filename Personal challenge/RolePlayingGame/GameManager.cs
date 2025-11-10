using System;

namespace RolePlayingGame
{
    public class GameManager
    {
        private Player hero;
        private Enemy enemy;
        private Shop shop;

        public GameManager(Player player, Shop shop, Enemy enemy)
        {
            this.hero = player;
            this.shop = shop;
            this.enemy = enemy;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\n ==== MainMenu ===");
                Console.WriteLine("1. Start Battle");
                Console.WriteLine("2. Inventory Menu");
                Console.WriteLine("3. Visit Shop");
                Console.WriteLine("4. Exit");

                Console.Write("Choose: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        StateBattle();
                        break;
                    case "2":
                        InventoryMenu();
                        break;
                    case "3":
                        VisitShop();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Enter input again!!!");
                        break;
                }    
            }
        }

        private void StateBattle()
        {
            Console.WriteLine("Battle Starts");

            while (hero.IsAlive() && enemy.IsAlive())
            {
                hero.Attack(enemy);

                if (!enemy.IsAlive())
                    break;

                enemy.Attack(hero);
            }

            Console.WriteLine(hero.IsAlive()
                ? $"{hero.Name} wins the fight"
                : $"{enemy.Name} defeat {hero.Name}!");

            Thread.Sleep(500);

            hero.Resethealth();
            enemy.Resethealth();
            Console.WriteLine($"{hero.Name}'s health has been restored to {hero.Health}!");
        }

        private void VisitShop()
        {
            shop.DisplayItems();
            Console.WriteLine($"{hero.Name} has {hero.Gold} Gold");
            Console.Write("Enter 1 to Buy an Item or 2 to Sell an Item: ");
            var option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("Select an Item you want to buy(0 to cancel): ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= shop.Stocks.Count)
                {
                    var item = shop.Stocks[choice - 1];
                    shop.BuyItem(hero, item);
                }

                else
                {
                    Console.WriteLine("Invalid Input!!!");
                }       
            }
            else if (option == "2")
            {
                hero.Inventory.ShowInventory();
                Console.Write("Select an item you want to Sell from your Inventory: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= hero.Inventory.items.Count)
                {
                    var item = hero.Inventory.items[choice - 1];
                    shop.SellItem(hero, item);
                }

                else
                {
                    Console.WriteLine("Invalid Input");
                }

            }
        }

        private void InventoryMenu()
        {
            bool inInventory = true;
            while(inInventory)
            {
                Console.WriteLine("\n ==== Inventory Menu ===");
                Console.WriteLine("1. Display Items Owned");
                Console.WriteLine("2. Equip Item");
                Console.WriteLine("3. Unequip Item");
                Console.WriteLine("4. Back to Start Menu");

                Console.Write("Choose: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        hero.Inventory.ShowInventory();
                        break;
                    case "2":
                        EquipItem();
                        break;
                    case "3":
                        UnequipItem();
                        break;
                    case "4":
                        Start();
                        break;
                    default:
                        Console.WriteLine("Enter input again!!!");
                        break;
                }
            }
            
        }
        private void EquipItem()
        {
            hero.Inventory.ShowInventory();
            Console.Write("Choose Item you want to Equip(0 to cancel): ");

            
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= hero.Inventory.items.Count)
            {
                hero.EquipItem(hero.Inventory.items[choice - 1]);
            }
        }

        private void UnequipItem()
        {
            if (hero.EquippedWeapon == null && hero.EquippedArmor == null && hero.EquippedPortion == null)
            {
                Console.WriteLine("No Item Equipped!!! Try Equiping some item");
                return;
            }

            int optionNumber = 1;
            if (hero.EquippedWeapon != null)
            {
                Console.WriteLine($"{optionNumber}. {hero.EquippedWeapon.Name} - Value: {hero.EquippedWeapon.Value}");
                optionNumber++;
            }
            if (hero.EquippedArmor != null)
            {
                Console.WriteLine($"{optionNumber}. {hero.EquippedArmor.Name} - Value: {hero.EquippedArmor.Value}");
                optionNumber++;
            }
            if (hero.EquippedPortion != null)
            {
                Console.WriteLine($"{optionNumber}. {hero.EquippedPortion.Name} - Value: {hero.EquippedPortion.Value}");
            }

            Console.Write("Choose an Item to Unequip(Enter 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                int currentOption = 1;
                if (hero.EquippedWeapon != null && choice == currentOption)
                {
                    hero.UnequipItem(hero.EquippedWeapon);
                    return;
                }
                if (hero.EquippedArmor != null && choice == (hero.EquippedWeapon != null ? 2 : 1))
                {
                    hero.UnequipItem(hero.EquippedArmor);
                    return;
                }
                if (hero.EquippedPortion != null && choice == (hero.EquippedWeapon != null && hero.EquippedArmor != null ? 3 : hero.EquippedWeapon != null || hero.EquippedArmor != null ? 2 : 1))
                {
                    hero.UnequipItem(hero.EquippedPortion);
                    return;
                }
                if (choice == 0)
                {
                    Console.WriteLine("Cancelled Operation");
                    return;
                }
                Console.WriteLine("Invalid Choice");
            }
        }
    }
}