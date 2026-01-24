using G00DS0ULRPG.Models;
using G00DS0ULRPG.Models.Shared;
using Newtonsoft.Json.Linq;

namespace G00DS0ULRPG.Services
{
    public static class GameDetailsService
    {
        public static GameDetails ReadGameDetails()
        {
            var gameDetailsJson = JObject.Parse(File.ReadAllText(".\\GameData\\GameDetails.json"));

            var gameDetails = new GameDetails(gameDetailsJson.StringValueOf("Title"),
                gameDetailsJson.StringValueOf("SubTitle"), gameDetailsJson.StringValueOf("Version"));

            foreach (var token in gameDetailsJson["PlayerAttributes"])
            {
                gameDetails.PlayerAttributes.Add(new PlayerAttribute(token.StringValueOf("Key"),
                    token.StringValueOf("DisplayName"), token.StringValueOf("DiceNotation")));
            }

            if (gameDetailsJson["Races"] != null)
            {
                foreach (var token in gameDetailsJson["Races"])
                {
                    var race = new Race
                    {
                        Key = token.StringValueOf("Key"),
                        DisplayName = token.StringValueOf("DisplayName")
                    };

                    if (token["PlayerAttributeModifiers"] != null)
                    {
                        foreach (var childToken in token["PlayerAttributeModifiers"])
                        {
                            race.PlayerAttributeModifiers.Add(new PlayerAttributeModifier
                            {
                                AttributeKey = childToken.StringValueOf("Key"),
                                Modifier = childToken.IntValueOf("Modifier")
                            });
                        }
                    }

                    gameDetails.Races.Add(race);
                }
            }

            return gameDetails;
        }
    }
}
