using UnityEngine;
using System;

namespace FPSRPGPrototype.Items
{

    [Serializable]
    public class WeaponItemRanged : WeaponItem
    {
        public GameObject projectile;
        public int mana;

        public WeaponItemRanged() :
            base()
        {
            itemType = ItemTypes.RangedWeapon;
        }
    }


}
