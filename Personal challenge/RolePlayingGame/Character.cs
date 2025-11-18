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
            target.TakeDamage(Strength);
        }

        public virtual void TakeDamage(int damage)
        {
            int finalDamage = Math.Max(0, damage - Defence);
            Health -= finalDamage;
            if (Health < 0)
                Health = 0;

            ConsoleUI.PrintColor($"{Name} took {finalDamage} damage! Remaining health: {Health}", ConsoleColor.Red);
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