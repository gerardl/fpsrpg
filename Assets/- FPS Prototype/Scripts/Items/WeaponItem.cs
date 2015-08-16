using System;
using UnityEngine;

namespace FPSRPGPrototype.Items
{
    [Serializable]
    public class WeaponItem : BaseClasses.Item
    {
        public GameObject model;

        public WeaponItem()
        {
        }

        public override void Activate()
        {
            base.Activate();
            //SoundController.Play("equip");
            //BaseClasses.Player.Instance.inventory.SetWeapon(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            //PlayerController.Instance.inventory.AddItem(this);
        }

        public static int GetAnimationId(WeaponItem item)
        {
            //if (item.itemType == ItemTypes.Staff) return 0;
            //if (item.itemType == ItemTypes.WeaponMelee)
            //{
            //    ItemWeaponMelee weapon = (ItemWeaponMelee)item;
            //    if (weapon.weaponMeleeType == ItemWeaponMelee.WeaponMeleeTypes.Sword) return 1;
            //    if (weapon.weaponMeleeType == ItemWeaponMelee.WeaponMeleeTypes.Axe) return 2;
            //}
            return 0;
        }
    }

}
