using UnityEngine;
using System;


namespace FPSRPGPrototype.BaseClasses
{
    [Serializable]
    public class Item
    {
        public enum ItemTypes
        {
            Quest = 1,
            Potion = 2,
            Food = 3,
            MeleeWeapon = 4,
            RangedWeapon = 5,
            Wearable = 6,
            Material = 7
        }

        public enum RarityLevels
        {
            Common = 1,
            Rare = 2
        }

        public string id;
        public string name;
        public RarityLevels rarity;
        public string description;
        public ItemTypes itemType;

        public virtual void Activate() { }
        public virtual void Deactivate() { }

        public Item() { }

        public override string ToString()
        {
            return itemType.ToString() + "_" + name;
        }

    }
}
