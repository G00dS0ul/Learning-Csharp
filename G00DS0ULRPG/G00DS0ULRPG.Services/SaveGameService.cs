using G00DS0ULRPG.Services.Factories;
using G00DS0ULRPG.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace G00DS0ULRPG.Services
{
    public static class SaveGameService
    {
        public static void Save(GameState gameState, string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(gameState, Formatting.Indented));
        }

        public static GameState LoadLastSaveOrCreateNew(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"FileName: {fileName}");
            }

            try
            {
                var data = JObject.Parse(File.ReadAllText(fileName));

                var player = CreatePlayer(data);

                var x = (int)data[nameof(GameState.XCoordinate)];
                var y = (int)data[nameof(GameState.YCoordinate)];

                return new GameState(player, x, y);
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error reading: {fileName}");
            }
        }

        private static Player CreatePlayer(JObject data)
        {
            var player = new Player((string)data[nameof(GameState.Player)][nameof(Player.Name)],
                        (int)data[nameof(GameState.Player)][nameof(Player.ExperiencePoints)],
                        (int)data[nameof(GameState.Player)][nameof(Player.MaximumHitPoints)],
                        (int)data[nameof(GameState.Player)][nameof(Player.CurrentHitPoints)],
                        GetPlayerAttributes(data),
                        (int)data[nameof(GameState.Player)][nameof(Player.Gold)]);
                   
            PopulatePlayerInventory(data, player);
            PopulatePlayerQuests(data, player);
            PopulatePlayerRecipes(data, player);

            return player;
        }

        private static IEnumerable<PlayerAttribute> GetPlayerAttributes(JObject data)
        {
            List<PlayerAttribute> attributes = [];

            foreach(var itemToken in (JArray)data[nameof(GameState.Player)][nameof(Player.Attributes)])
            {
                attributes.Add(new PlayerAttribute((string)itemToken[nameof(PlayerAttribute.Key)],
                    (string)itemToken[nameof(PlayerAttribute.DisplayName)],
                    (string)itemToken[nameof(PlayerAttribute.DiceNotation)],
                    (int)itemToken[nameof(PlayerAttribute.BaseValue)],
                    (int)itemToken[nameof(PlayerAttribute.ModifiedValue)]));
            }

            return attributes;
        }

        private static void PopulatePlayerInventory(JObject data, Player player)
        {
            foreach (var itemToken in (JArray)data[nameof(GameState.Player)][nameof(Player.Inventory)][nameof(Inventory.Items)])
            {
                var itemId = (int)itemToken[nameof(GameItem.ItemTypeID)];

               player.AddItemToInventory(ItemFactory.CreateGameItem(itemId));
            }
        }

        private static void PopulatePlayerQuests(JObject data, Player player)
        {
            foreach (var questToken in
                            (JArray)data[nameof(GameState.Player)][nameof(Player.Quests)])
            {
                var questId =
                    (int)questToken[nameof(QuestStatus.PlayerQuest)][nameof(QuestStatus.PlayerQuest.ID)];
                var quest = QuestFactory.GetQuestByID(questId);
                var questStatus = new QuestStatus(quest)
                {
                    IsComplete = (bool)questToken[nameof(QuestStatus.IsComplete)]
                };

                player.Quests.Add(questStatus);
            }
        }

        private static void PopulatePlayerRecipes(JObject data, Player player)
        {
            foreach (var recipeToken in (JArray)data[nameof(GameState.Player)][nameof(Player.Recipes)])
            {
                var recipeId = (int)recipeToken[nameof(Recipe.ID)];

                var recipe = RecipeFactory.GetRecipeByID(recipeId);

                player.Recipes.Add(recipe);
            }

        }

    }
}
