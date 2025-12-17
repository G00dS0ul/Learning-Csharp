using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {
            Trader innerCircle = new Trader("Inner Circle Trader");
            innerCircle.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            Trader blackFarmer = new Trader("BlackFarmer Circle");
            blackFarmer.AddItemToInventory(ItemFactory.CreateGameItem(1002));

            Trader theEmptyPocket = new Trader("The Empty Pocket Circle");
            theEmptyPocket.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            AddTraderToList(innerCircle);
            AddTraderToList(blackFarmer);
            AddTraderToList(theEmptyPocket);
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if(_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'");
            }

            _traders.Add(trader);
        }
    }
}
