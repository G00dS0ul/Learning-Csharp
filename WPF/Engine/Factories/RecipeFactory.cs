using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = [];

        static RecipeFactory()
        {
            var chewChewBar = new Recipe(1, "Chew Chew Bar");
            chewChewBar.AddIngredient(3001, 1);
            chewChewBar.AddIngredient(3002, 1);
            chewChewBar.AddIngredient(3003, 1);
            chewChewBar.AddOutputItem(2001, 1);

            _recipes.Add(chewChewBar);
        }

        public static Recipe? GetRecipeByID(int id)
        {
            return _recipes.FirstOrDefault(r => r.ID == id);
        }
    }
}
