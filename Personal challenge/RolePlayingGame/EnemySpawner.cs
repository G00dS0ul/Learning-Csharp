namespace RolePlayingGame
{
    public class EnemySpawner
    {
        private List<Enemy> enemyType = new List<Enemy>()
        {
            new Enemy("Goblin", "Beast", 100, 5, 10, 10),
            new Enemy("Witch", "Mage", 100, 5, 15, 15),
            new Enemy("Dark Wizard", "Mage", 100, 10, 30, 20),
            new Enemy("Troll", "Giant", 100, 50, 50, 30),
            new Enemy("Skeleton", "Undead", 100, 5, 5, 5),
            new Enemy("Zombie", "Undead", 100, 5, 10, 8),
            new Enemy("Orc", "Beast", 100, 10, 10, 12)
        };

        public Enemy EnemyGenerator()
        {
            Random rnd = new Random();
            Enemy rndEnemy = enemyType[rnd.Next(enemyType.Count)];

            Enemy newEnemy = new Enemy(
                rndEnemy.Name, rndEnemy.Type, rndEnemy.Health, rndEnemy.Defence, rndEnemy.Strength, rndEnemy.XpReward);



            return newEnemy;
        }
    }
}