namespace RolePlayingGame
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Defence { get; set; }
        public int Strength { get; set; }

        void Attack(ICharacter target);
        void TakeDamage(int damage);
        bool IsAlive();
    }
}