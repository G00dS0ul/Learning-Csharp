using System.Reflection.PortableExecutable;
using static RolePlayingGame.Program;

namespace RolePlayingGame
{
    public class Player : Character
    {
        public int Experience { get; set; }
        public int Level { get; set; }

        public override void Attack(Character target)
        {
            Random rand = new Random();
            bool isCritical = rand.Next(1, 101) <= 20;
            int damage = Strength - target.Defence;

            if (isCritical)
            {
                damage *= 2;
                Console.WriteLine("Critical Hit With Twice the damage");
            }
            
            damage = Math.Max(0, damage);
            target.Health -= damage;
            Console.WriteLine($"{Name} hit {target.Name} for {damage} damage!");

            if (!target.IsAlive())
            {
                Console.WriteLine($"{target.Name} has been defeated");

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