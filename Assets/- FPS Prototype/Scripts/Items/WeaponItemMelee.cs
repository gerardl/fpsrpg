using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPSRPGPrototype.Items
{
    public class WeaponItemMelee : WeaponItem
    {
        public enum MeleeWeaponTypes
        {
            Sword = 1
        }


        public Combat.DamageConfig damage;
        public MeleeWeaponTypes meleeWeaponType;

        public WeaponItemMelee() :
            base()
        {
            itemType = ItemTypes.MeleeWeapon;
            meleeWeaponType = MeleeWeaponTypes.Sword;
            damage = new Combat.DamageConfig();
        }
    }
}
