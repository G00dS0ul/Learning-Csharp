using Engine.Actions;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems = [];

        static ItemFactory()
        {
            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);
            BuildWeapon(1003, "Excalibur", 10, 3, 5);

            BuildWeapon(1501, "Snake Fangs", 0, 0,  2);
            BuildWeapon(1502, "Rat Claws", 0, 0,  2);
            BuildWeapon(1503, "Spider Fangs", 0, 0,  4);

            BuildHealingItem(2001, "Chew Chew Bar", 5, 2);

            BuildMiscellaneousItem(3001, "Oats", 1);
            BuildMiscellaneousItem(3002, "Honey", 4);
            BuildMiscellaneousItem(3003, "Raisins", 5);



            BuildMiscellaneousItem(9001, "Snake Fang", 1);
            BuildMiscellaneousItem(9002, "Snake skin", 2);
            BuildMiscellaneousItem(9003, "Rat Tail", 1);
            BuildMiscellaneousItem(9004, "Rat Fur", 2);
            BuildMiscellaneousItem(9005, "Spider Fang", 1);
            BuildMiscellaneousItem(9006, "Spider Silk", 2);
        }   

        public static GameItem? CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }
        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price, int minimumDamage, int maximumDamage)
        {
            var weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);

            _standardGameItems.Add(weapon);
        }

        private static void BuildHealingItem(int id, string name, int hitPointsToHeal, int price)
        {
            var healingItem = new GameItem(GameItem.ItemCategory.Consumable, id, name, price);
            healingItem.Action = new Heal(healingItem, hitPointsToHeal);
            _standardGameItems.Add(healingItem);
        }

        public static string ItemName(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(i => i.ItemTypeID == itemTypeID)?.Name ?? "";
        }
    }
}
