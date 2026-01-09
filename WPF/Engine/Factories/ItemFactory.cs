using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using Engine.Actions;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";

        private static readonly List<GameItem> _standardGameItems = [];

        static ItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                var data = new XmlDocument();
                data.LoadXml((File.ReadAllText(GAME_DATA_FILENAME)));

                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/HealingItems/HealingItem"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/MiscellaneousItems/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: '{GAME_DATA_FILENAME}'");
            }

        }   

        public static GameItem? CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }
        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price, int minimumDamage, int maximumDamage)
        {
            var weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);

            _standardGameItems.Add(weapon);
        }

        private static void BuildHealingItem(int id, string name, int hitPointsToHeal, int price)
        {
            var healingItem = new GameItem(GameItem.ItemCategory.Consumable, id, name, price);
            healingItem.Action = new Heal(healingItem, hitPointsToHeal);
            _standardGameItems.Add(healingItem);
        }

        public static string ItemName(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(i => i.ItemTypeID == itemTypeID)?.Name ?? "";
        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                var itemCategory = DetermineItemCategory(node.Name);

                var gameItem = new GameItem(itemCategory, GetXmlAttributeAsInt(node, "ID"),
                    GetXmlAttributeAsString(node, "Name"),
                    GetXmlAttributeAsInt(node, "Price"),
                    itemCategory == GameItem.ItemCategory.Weapon);

                if (itemCategory == GameItem.ItemCategory.Weapon)
                {
                    gameItem.Action = new AttackWithWeapon(gameItem, GetXmlAttributeAsInt(node, "MinimumDamage"),
                        GetXmlAttributeAsInt(node, "MaximumDamage"));
                }
                else if (itemCategory == GameItem.ItemCategory.Consumable)
                {
                    gameItem.Action = new Heal(gameItem, GetXmlAttributeAsInt(node, "HitPointsToHeal"));
                }

                _standardGameItems.Add(gameItem);
            }

            
        }

        private static GameItem.ItemCategory DetermineItemCategory(string itemType)
        {
            return itemType switch
            {
                "Weapon" => GameItem.ItemCategory.Weapon,
                "HealingItem" => GameItem.ItemCategory.Consumable,
                _ => GameItem.ItemCategory.Miscellaneous,
            };
        }

        private static int GetXmlAttributeAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(GetXmlAttribute(node, attributeName));
        }

        private static string GetXmlAttributeAsString(XmlNode node, string attributeName)
        {
            return GetXmlAttribute(node, attributeName);
        }

        private static string GetXmlAttribute(XmlNode node, string attributeName)
        {
            var attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist!!!");
            }

            return attribute.Value;
        }
    }
}
