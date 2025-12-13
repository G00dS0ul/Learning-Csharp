using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class GameSession
    {
        public Player? CurrentPlayer { get; set; }
        public Location? CurrentLocation { get; set; }
        public World CurrentWorld { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player
            {
                Name = "Iche",
                CharacterClass = "Fighter",
                HitPoints = 10,
                Gold = 100000,
                ExperiencePoints = 0,
                Level = 1
            };

            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(-2, -1);
        }
    }
}
