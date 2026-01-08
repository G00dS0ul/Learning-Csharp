using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private string? _characterClass;
        private int _experiencePoints;
        

        public string? CharacterClass 
        {
            get
            {
                return _characterClass;
            } 
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }
        
        public int ExperiencePoints
        { 
            get { return _experiencePoints; }
            set 
            { 
                _experiencePoints = value;
                OnPropertyChanged();

                SetLevelAndMaximumPoints();
            } 
        }

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }
        #endregion

        public event EventHandler? OnLeveledUp;

        public Player(string name, string characterClass, int experiencePoints, int maximumHitPoints, int currentHitPoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold) 
        {
            _characterClass = characterClass;
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
