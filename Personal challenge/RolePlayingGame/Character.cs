namespace RolePlayingGame
{
    public class Character
    {
        public string? Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; }
        public int Defence { get; set; }
        public int Strength { get; set; }

        public Character()
        {
            Health = 100;
            MaxHealth = 100;
        }

        public virtual void Attack(ICharacter target)
        {
            int damage = Math.Max(0, Strength - target.Defence);
            target.Health -= damage;
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

        public void Resethealth()
        {
            Health = MaxHealth;
        }
    }
}