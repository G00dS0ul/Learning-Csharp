namespace RolePlayingGame
{
    class Enemy : Character
    {
        public string Type { get; set; }

        public override void Attack(Character target)
        {
            int damage = Math.Max(0, Strength - target.Defence);
            target.Health -= damage;
            Console.WriteLine($"{Name} ({Type}) bites {target.Name} for {damage} damage!");
        }
    }
}