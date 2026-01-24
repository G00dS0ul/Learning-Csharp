using System.Xml;
using G00DS0ULRPG.Models;
using G00DS0ULRPG.Models.Shared;

namespace G00DS0ULRPG.Services.Factories
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

        private static void LoadRecipesFromNodes(XmlNodeList? nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                var ingredients = new List<ItemQuantity>();

                XmlNodeList? ingredientNodes = node.SelectNodes("./Ingredients/Item");
                if (ingredientNodes != null)
                {
                    foreach (XmlNode childNode in ingredientNodes)
                    {
                        var item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));

                        if (item != null)
                        {
                            ingredients.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                        }
                    }
                }

                var outputItems = new List<ItemQuantity>();

                XmlNodeList? outputItemNodes = node.SelectNodes("./OutputItem/Item");
                if (outputItemNodes != null)
                {
                    foreach (XmlNode childNode in outputItemNodes)
                    {
                        var item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));

                        if (item != null)
                        {
                            outputItems.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                        }
                    }
                }

                var recipe = new Recipe(node.AttributeAsInt("ID"), node.SelectSingleNode("./Name")?.InnerText ?? "", ingredients, outputItems);
                _recipes.Add(recipe);
            }
        }

        public static Recipe? GetRecipeByID(int id)
        {
            return _recipes.FirstOrDefault(r => r.ID == id);
        }
    }
}
