using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;
using Engine.Models;
using Engine.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine.Services
{
    public static class SaveGameService
    {
        public static void Save(GameSession gameSession, string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(gameSession, Formatting.Indented));
        }

        public static GameSession LoadLastSaveOrCreateNew(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new GameSession();
            }

            try
            {
                var data = JObject.Parse(File.ReadAllText(fileName));

                var player = CreatePlayer(data);

                var x = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.XCoordinate)];
                var y = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.YCoordinate)];

                return new GameSession(player, x, y);
            }
            catch (Exception ex)
            {
                return new GameSession();
            }
        }

        private static Player CreatePlayer(JObject data)
        {
            var fileVersion = FileVersion(data);

            Player player;

            switch (fileVersion)    
            {
                case "0.1.000":
                    player = new Player((string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Name)],
                        (string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CharacterClass)],
                        (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.ExperiencePoints)],
                        (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.MaximumHitPoints)],
                        (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CurrentHitPoints)],
                        (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Dexterity)],
                        (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Gold)]);
                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }

            PopulatePlayerInventory(data, player);
            PopulatePlayerQuests(data, player);
            PopulatePlayerRecipes(data, player);

            return player;
        }

        private static void PopulatePlayerInventory(JObject data, Player player)
        {
            var fileVersion = FileVersion(data);

            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (var itemToken in (JArray)data[nameof(GameSession.CurrentPlayer)][
                                 nameof(Player.Inventory)][nameof(Inventory.Items)])
                    {
                        var itemId = (int)itemToken[nameof(GameItem.ItemTypeID)];

                        player.AddItemToInventory(ItemFactory.CreateGameItem(itemId));
                    }

                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
        }

        private static void PopulatePlayerQuests(JObject data, Player player)
        {
            var fileVersion = FileVersion(data);

            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (var questToken in
                             (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Quests)])
                    {
                        var questId =
                            (int)questToken[nameof(QuestStatus.PlayerQuest)][nameof(QuestStatus.PlayerQuest.ID)];
                        var quest = QuestFactory.GetQuestByID(questId);
                        var questStatus = new QuestStatus(quest);
                        questStatus.IsComplete = (bool)questToken[nameof(QuestStatus.IsComplete)];

                        player.Quests.Add(questStatus);
                    }

                    break;
                default:
                    throw new InvalidDataException($"File Version '{fileVersion}' not recognized");
            }
        }

        private static void PopulatePlayerRecipes(JObject data, Player player)
        {
            var fileVersion = FileVersion(data);

            switch (fileVersion)
            {
                case "0.1.000":
                    foreach (var recipeToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Recipes)])
                    {
                        var recipeId = (int)recipeToken[nameof(Recipe.ID)];

                        var recipe = RecipeFactory.GetRecipeByID(recipeId);

                        player.Recipes.Add(recipe);
                    }

                    break;
                default:
                    throw new InvalidDataException($"File version '{fileVersion}' not recognized");
            }
        }

        private static string FileVersion(JObject data)
        {
            return(string)data[nameof(GameSession.Version)];
        }
    }
}
