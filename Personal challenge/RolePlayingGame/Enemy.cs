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
     
            base.Attack(target);
            Console.WriteLine($"{Name} ({Type}) attacks {target.Name} for {Strength} damage!");
            Thread.Sleep(500);
        }
    }
}