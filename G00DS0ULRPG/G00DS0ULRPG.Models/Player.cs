using System.Collections.ObjectModel;

namespace G00DS0ULRPG.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private int _experiencePoints;
        
        public int ExperiencePoints
        { 
            get => _experiencePoints;
            set 
            { 
                _experiencePoints = value;

                SetLevelAndMaximumPoints();
            } 
        }

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }
        #endregion

        public event EventHandler? OnLeveledUp;

        public Player(string name, int experiencePoints, int maximumHitPoints, int currentHitPoints, IEnumerable<PlayerAttribute> attributes, int gold) :
            base(name, maximumHitPoints, currentHitPoints, attributes, gold) 
        {
            _experiencePoints = experiencePoints;

            Quests = [];
            Recipes = [];
        }


        public void AddExperience (int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }
        

        private void SetLevelAndMaximumPoints()
        {
            var originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if(Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
