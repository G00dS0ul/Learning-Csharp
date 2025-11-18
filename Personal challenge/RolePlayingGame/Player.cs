using System.Reflection.PortableExecutable;
using static RolePlayingGame.Program;
using static System.Net.Mime.MediaTypeNames;

namespace RolePlayingGame
{
    public class Player : Character, ICharacter
    {
        public int Experience { get; set; }
        public int Level { get; set; }
        public Inventory Inventory { get; private set; }
        public Item? EquippedWeapon { get; set; }
        public Item? EquippedArmor { get; set; }
        public Item? EquippedPortion { get; set; }
        public float Gold { get; set; }

        public Player()
        {
            Inventory = new Inventory();
        }

        public override void Attack(ICharacter target)
        {
            Random rand = new Random();
            bool isCritical = rand.Next(1, 101) <= 20;

            var totalAttack = Strength;
            var weaponBonus = 0;
            if (EquippedWeapon != null)
            {
                weaponBonus = EquippedWeapon.Value;
                totalAttack += weaponBonus;
            }

            if (isCritical)
            {
                totalAttack *= 2;
                ConsoleUI.PrintColor($"Critical Hit With Twice the damage{totalAttack}", ConsoleColor.DarkRed);
            }

            target.TakeDamage(totalAttack);
            ConsoleUI.PrintColor($"{Name} attacks {target.Name} for {totalAttack} damage!", ConsoleColor.Red);
            Thread.Sleep(500);

            if (!target.IsAlive())
            {
                ConsoleUI.PrintColor($"{target.Name} has been defeated", ConsoleColor.Blue);

            }
        }

        public override void TakeDamage(int damage)
        {
            var totalDefence = Defence;
            var armorBonus = 0;
            if (EquippedArmor != null)
            {
                armorBonus = EquippedArmor.Value;
                totalDefence += armorBonus;
            }

            var finalDamage = Math.Max(0, damage - totalDefence);
            Health -= finalDamage;
            ConsoleUI.PrintColor($"{Name} took {finalDamage} damage!  Remaining health: {Health}", ConsoleColor.DarkMagenta);
        }

        public void UsePortion()
        {
            if (EquippedPortion != null)
            {
                Health += EquippedPortion.Value;
                ConsoleUI.PrintColor($"Player's Health is now {Health}", ConsoleColor.Green);
            }
        }

        public void EquipItem(Item item)
        {
            if (item.Type == "Weapon" && EquippedWeapon == null)
            {
                EquippedWeapon = item;
                ConsoleUI.PrintColor($"{EquippedWeapon.Name} Equipped, Strength increased by {EquippedWeapon.Value}!", ConsoleColor.Red);
                ConsoleUI.PrintColor($"{Name} Current Strenght is {Strength}", ConsoleColor.Red);
            }

            else if (item.Type == "Armor" && EquippedArmor == null)
            {
                EquippedArmor = item;
                ConsoleUI.PrintColor($"{EquippedArmor.Name} Equipped, Defence increased by {EquippedArmor.Value}!", ConsoleColor.DarkYellow);
                ConsoleUI.PrintColor($"{Name} Current Defence is {Defence}", ConsoleColor.DarkYellow);
            }

            else if (item.Type == "Portion")
            {
                EquippedPortion = item;
                UsePortion();
                ConsoleUI.PrintColor($"{EquippedPortion.Name} Equipped, Health increased by {EquippedPortion.Value}", ConsoleColor.Green);
                ConsoleUI.PrintColor($"{Name} Current Health is {Health}", ConsoleColor.Green);
                Inventory.RemoveItem(item);
                EquippedPortion = null;
            }

            else
            {
                ConsoleUI.PrintColor("An Item is been Equipped, Unequip the item before equiping new Item", ConsoleColor.DarkCyan);
            }

        }

        public void UnequipItem(Item item)
        {
            if(item.Type == "Weapon")
            {
                Strength -= item.Value;
                EquippedWeapon = null;
                ConsoleUI.PrintColor($"{item.Name} has been unequipped, Strength value has decreased by {item.Value}", ConsoleColor.Red);
            }

            if(item.Type == "Armor")
            {
                Defence -= item.Value;
                EquippedArmor = null;
                ConsoleUI.PrintColor($"{item.Name} has been unequipped, Defence value has decreased by {item.Value}", ConsoleColor.DarkYellow);
            }

            if(item.Type == "Portion")
            {
                Health -= item.Value;
                Inventory.AddItem(item);
                EquippedPortion = null;
                ConsoleUI.PrintColor($"{item.Name} has been unequipped, Health value has decreased by {item.Value}", ConsoleColor.Green);
            }
                
        }

        private void GainExperience(int amount)
        {
            Experience += amount;

            if (Experience >= 100)
            {
                Experience = 0;
                Level++;
                Strength += 5;
                Defence += 2;
                Health += 10;
                Console.WriteLine($"{Name} has leveled up to Level {Level}");
            }
        }

        
    }
}