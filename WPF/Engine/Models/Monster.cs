using Engine.Factories;
using Engine.Services;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        private readonly List<ItemPercentage> _lootTable = [];

        public int ID { get; }
        public string? ImageName { get; }
        public int RewardExperiencePoints { get; }

        public Monster(int id, string? name, string? imageName, int maximumHitPoints, IEnumerable<PlayerAttribute> attributes, GameItem currentWeapon, int rewardExperiencePoints, int gold)
            : base(name, maximumHitPoints, maximumHitPoints, attributes, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            _lootTable.RemoveAll(ip => ip.ID == id);

            _lootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster GetNewInstance()
        {
            var newMonster = new Monster(ID, Name, ImageName, MaximumHitPoints,  Attributes, CurrentWeapon, RewardExperiencePoints,
                Gold);

            foreach (var itemPercentage in _lootTable)
            {
                newMonster.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);

                if (DiceService.Instance.Roll(100).Value <= itemPercentage.Percentage)
                {
                    newMonster?.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }
    }
}
