namespace RolePlayingGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var hero = new Player
            {
                Name = "Iche",
                Health = 100,
                Defence = 5,
                Strength = 20,
                Level = 1
            };

            var enemy = new Enemy
            {
                Name = "Goblin",
                Type = "Super Natural",
                Health = 60,
                Strength = 10,
                Defence = 3
            };

            Console.WriteLine("Battle Starts");

            while(hero.IsAlive() &&  enemy.IsAlive())
            {
                hero.Attack(enemy);

                if (!enemy.IsAlive())
                    break;

                enemy.Attack(hero);
            }

            Console.WriteLine(hero.IsAlive()
                ? $"{hero.Name} wins the fight"
                : $"{enemy.Name} defeat {hero.Name}!");


        }
    }
}