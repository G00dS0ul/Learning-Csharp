namespace RolePlayingGame
{
    public class Weapon
    {
        public string WeaponName;
        public int WeaponDamage;
        public string WeaponType;

        public Weapon(string weaponName, int weaponDamage, string weaponType)
        {
            WeaponName = weaponName;
            WeaponDamage = weaponDamage;
            WeaponType = weaponType;
        }

        public virtual void Fire()
        {
            switch (WeaponType.ToLower())
            {
                case "Sniper":
                    Console.WriteLine($"Firing {WeaponName} for a long range distance battle. Damage: {WeaponDamage}");
                    break;
                case "ShotGun":
                    Console.WriteLine($"Firing {WeaponName} for a short range distance battle. Damage: {WeaponDamage}");
                    break;
                case "AssaultRiffle":
                    Console.WriteLine($"Firing {WeaponName} for a long range distance battle. Damage: {WeaponDamage}");
                    break;
                case "Laser":
                    Console.WriteLine($"Charging {WeaponName} for a mid range distance battle. Damage: {WeaponDamage}");
                    break;
                case "Melee":
                    Console.WriteLine($"Swinging {WeaponName} for a short range distance battle. Damage: {WeaponDamage}");
                    break;
                case "Punch":
                    Console.WriteLine($"Throwing a punch with {WeaponName} for a short range distance battle. Damage: {WeaponDamage}");
                    break;
                default: 
                    Console.WriteLine($"Uisng {WeaponName}! with damage {WeaponDamage}");
                    break;

            }

        }
    }
}