namespace RolePlayingGame
{
    public class Enemy : Character, ICharacter
    {
        public string? Type { get; set; }

        public Enemy(string? name, string? type, int health, int defence, int strength)
        {
            this.Name = name;
            this.Type = type;
            this.Health = health;
            this.Defence = defence;
            this.Strength = strength;
        }

        public override void Attack(ICharacter target)
        {
            int damage = Math.Max(0, Strength - target.Defence);
            target.TakeDamage(damage);
            Console.WriteLine($"{Name} ({Type}) attacks {target.Name} for {damage} damage!");
            Thread.Sleep(500);
        }
    }
}