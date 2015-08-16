using FPSRPGPrototype.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Items
{
    [Serializable]
    public class ItemLibrary : Utilities.Singleton<ItemLibrary>
    {
        public List<QuestItem> questItems = new List<QuestItem>();
        public List<WeaponItemMelee> meleeWeapons = new List<WeaponItemMelee>();
        public List<WeaponItemRanged> rangedWeapons = new List<WeaponItemRanged>();
        public List<WearableItem> wearables = new List<WearableItem>();
        public List<PotionItem> potions = new List<PotionItem>();

        public List<Item> Items
        {
            get
            {
                // keep this chached for a certain amount of time before
                // recombining lists?

                List<Item> list = new List<Item>();
                list.AddRange(questItems.ToArray());
                list.AddRange(meleeWeapons.ToArray());
                list.AddRange(rangedWeapons.ToArray());
                list.AddRange(wearables.ToArray());
                list.AddRange(potions.ToArray());
                return list;
            }
        }

        public Item GetItemById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            Item item = Items.Find(i => i.id == id);
            return item;
        }

        public void AddItem(Item.ItemTypes type)
        {
            switch (type)
            {
                case Item.ItemTypes.Quest:
                    questItems.Add(new QuestItem());
                    break;
                case Item.ItemTypes.Potion:
                    potions.Add(new PotionItem());
                    break;
                case Item.ItemTypes.Food:
                    break;
                case Item.ItemTypes.MeleeWeapon:
                    meleeWeapons.Add(new WeaponItemMelee());
                    break;
                case Item.ItemTypes.RangedWeapon:
                    rangedWeapons.Add(new WeaponItemRanged());
                    break;
                case Item.ItemTypes.Wearable:
                    wearables.Add(new WearableItem());
                    break;
                case Item.ItemTypes.Material:
                    break;
                default:
                    break;
            }
        }

        public void RemoveItem(Item item)
        {
            switch (item.itemType)
            {
                case Item.ItemTypes.Quest:
                    questItems.Remove((QuestItem)item);
                    break;
                case Item.ItemTypes.Potion:
                    potions.Remove((PotionItem)item);
                    break;
                case Item.ItemTypes.Food:
                    break;
                case Item.ItemTypes.MeleeWeapon:
                    meleeWeapons.Remove((WeaponItemMelee)item);
                    break;
                case Item.ItemTypes.RangedWeapon:
                    rangedWeapons.Remove((WeaponItemRanged)item);
                    break;
                case Item.ItemTypes.Wearable:
                    wearables.Remove((WearableItem)item);
                    break;
                case Item.ItemTypes.Material:
                    break;
                default:
                    break;
            }
        }
    }
}
