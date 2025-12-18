namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        public string? ImageName { get; private set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }

        public int RewardExperiencePoints { get; private set; }

        public Monster(string name, string imageName, int maximumPoints, int hitPoints, int minimumDamage, int maximumDamage, int rewardExperiencePoints, int rewardGold)
        {
            this.Name = name;
            this.ImageName = $"/Engine;component/Images/Monster/{imageName}";
            this.MaximumHitPoints = maximumPoints;
            this.CurrentHitPoints = hitPoints;
            this.MinimumDamage = minimumDamage;
            this.MaximumDamage = maximumDamage;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.Gold = rewardGold;
        }
    }
}
