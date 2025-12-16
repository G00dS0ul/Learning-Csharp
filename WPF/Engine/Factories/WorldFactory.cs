using Engine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            newWorld.AddLocation(-2, -1, "Farmer's Field", "There are rows of corn growing here, with giant rats hiding between them", "/Engine;component/Images/Locations/FarmFields.png");

            newWorld.LocationAt(-2, -1).AddMonster(2, 100);

            newWorld.AddLocation(-1, -1, "Farmer's House", "This is the house of yout neighbor, Farmer Ted.", "/Engine;component/Images/Locations/FarmHouse.png");
            newWorld.AddLocation(0, -1, "Home", "This is your Home", "/Engine;component/Images/Locations/Home.png");
            newWorld.AddLocation(-1, 0, "Trading Shop", "The shop of the Inner Circle, the merchandise", "/Engine;component/Images/Locations/Trader.png");
            newWorld.AddLocation(0, 0, "Town Square", "You see a fountain here", "/Engine;component/Images/Locations/TownSquare.png");
            newWorld.AddLocation(1, 0, "Town Gate", "There is a gate here, protecting the town from giant", "/Engine;component/Images/Locations/TownGate.png");
            newWorld.AddLocation(2, 0, "Spider Forest", "The trees in this forest are covered with spider webs.", "/Engine;component/Images/Locations/SpiderForest.png");

            newWorld.LocationAt(2, 0).AddMonster(3, 100);

            newWorld.AddLocation(0, 1, "Herbalist's Hut", "You see a small hut, with plants drying from the roof.", "/Engine;component/Images/Locations/HerbalistsHut.png");

            newWorld.LocationAt(0, 1).QuestAvalaibleHere.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(0, 2, "Herbalist's Garden", "There are many plants here, with snakes hiding behind", "/Engine;component/Images/Locations/HerbalistsGarden.png");

            newWorld.LocationAt(0, 2).AddMonster(1, 100);

            return newWorld;
        }
    }
}
