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

            int totalAttack = Strength;
            if (EquippedWeapon != null)
            {
                totalAttack = Strength;
            }

            if (isCritical)
            {
                totalAttack *= 2;
                Console.WriteLine("Critical Hit With Twice the damage");
            }

            int damage = totalAttack - target.Defence;
            target.TakeDamage(damage);
            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            Thread.Sleep(500);

            if (!target.IsAlive())
            {
                Console.WriteLine($"{target.Name} has been defeated");

            }
        }

        public override void TakeDamage(int damage)
        {
            var totalDefence = Defence;

            if (EquippedArmor != null)
            {
                totalDefence = Defence + EquippedArmor.Value;
            }

            var finalDamage = damage - totalDefence;
            finalDamage = Math.Max(0, damage);
            Health -= finalDamage;
            Console.WriteLine($"{Name} took {damage} damage!  Remaining health: {Health}");
        }

        public void UsePortion()
        {
            if (EquippedPortion != null)
            {
                Health += EquippedPortion.Value;
                Console.WriteLine($"Player's Health is now {Health}");
            }
        }

        public void EquipItem(Item item)
        {
            if (item.Type == "Weapon" && EquippedWeapon == null)
            {
                EquippedWeapon = item;
                Strength += EquippedWeapon.Value;
                Console.WriteLine($"{EquippedWeapon.Name} Equipped, Strength increased by {EquippedWeapon.Value}!");
                Console.WriteLine($"{Name} Current Strenght is {Strength}");
            }

            else if (item.Type == "Armor" && EquippedArmor == null)
            {
                EquippedArmor = item;
                Defence += EquippedArmor.Value;
                Console.WriteLine($"{EquippedArmor.Name} Equipped, Defence increased by {EquippedArmor.Value}!");
                Console.WriteLine($"{Name} Current Defence is {Defence}");
            }

            else if (item.Type == "Portion")
            {
                EquippedPortion = item;
                UsePortion();
                Console.WriteLine($"{EquippedPortion.Name} Equipped, Health increased by {EquippedPortion.Value}");
                Console.WriteLine($"{Name} Current Health is {Health}");
                Inventory.RemoveItem(item);
            }

            else
            {
                Console.WriteLine("An Item is been Equipped, Unequip the item before equiping new Item");
            }

        }

        public void UnequipItem(Item item)
        {
            if(item.Type == "Weapon")
            {
                Strength -= item.Value;
                EquippedWeapon = null;
                Console.WriteLine($"{item.Name} has been unequipped, Strength value has decreased by {item.Value}");
            }

            if(item.Type == "Armor")
            {
                Defence -= item.Value;
                EquippedArmor = null;
                Console.WriteLine($"{item.Name} has been unequipped, Defence value has decreased by {item.Value}");
            }

            if(item.Type == "Portion")
            {
                Health -= item.Value;
                Inventory.AddItem(item);
                EquippedPortion = null;
                Console.WriteLine($"{item.Name} has been unequipped, Health value has decreased by {item.Value}");
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