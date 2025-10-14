namespace RolePlayingGame
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Defence { get; set; }
        public int Strength { get; set; }

        public virtual void Attack(Character target)
        {
            int damage = Math.Max(0, Strength - target.Defence);
            target.Health -= damage;
            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                Health = 0;

            Console.WriteLine($"{Name} took {damage} damage! Remaining health: {Health}");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}