namespace RolePlayingGame
{
    class Enemy : Character, ICharacter
    {
        public string Type { get; set; }

        public override void Attack(ICharacter target)
        {
            int damage = Math.Max(0, Strength - target.Defence);
            target.Health -= damage;
            Console.WriteLine($"{Name} ({Type}) bites {target.Name} for {damage} damage!");
        }
    }
}