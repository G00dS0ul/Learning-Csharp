using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Monster : BaseNotificationClass
    {
        private int _hitPoints;

        public string? Name { get; private set; }
        public string? ImageName { get; private set; }
        public int MaximumPoints { get; private set; }
        public int HitPoints
        {
            get 
            { 
                return _hitPoints; 
            }
            private set 
            { 
                _hitPoints = value;
                OnPropertyChanged(nameof(HitPoints));
            }
        }
        public int RewardExperiencePoints { get; private set; }
        public int RewardGold { get; private set; }
        public ObservableCollection<ItemQuantity> Inventory { get; set; }

        public Monster(string name, string imageName, int maximumPoints, int hitPoints, int rewardExperiencePoints, int rewardGold)
        {
            this.Name = name;
            this.ImageName = string.Format("/Engine;component/Images/Monster/{0}", imageName);
            this.MaximumPoints = maximumPoints;
            this.HitPoints = hitPoints;
            this.RewardExperiencePoints = rewardExperiencePoints;
            this.RewardGold = rewardGold;
            this.Inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}
