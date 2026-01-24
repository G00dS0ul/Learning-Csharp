using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Recipe.xml";
        private static readonly List<Recipe> _recipes = [];

        static RecipeFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadRecipesFromNodes(data.SelectNodes("/Recipes/Recipe"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data File: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadRecipesFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                var recipe = new Recipe(node.AttributeAsInt("ID"), node.SelectSingleNode("./Name")?.InnerText ?? "");

                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Item"))
                {
                    recipe.AddIngredient(childNode.AttributeAsInt("ID"), childNode.AttributeAsInt("Quantity"));
                }

                foreach (XmlNode childNode in node.SelectNodes("./OutputItem/Item"))
                {
                    recipe.AddOutputItem(childNode.AttributeAsInt("ID"), childNode.AttributeAsInt("Quantity"));
                }

                _recipes.Add(recipe);
            }
        }

        public static Recipe? GetRecipeByID(int id)
        {
            return _recipes.FirstOrDefault(r => r.ID == id);
        }
    }
}
