using System;
using System.Collections.Generic;
using FPSRPGPrototype.BaseClasses;
using UnityEngine;

namespace FPSRPGPrototype.Player
{
    public class InventoryController
    {
        public event Action<Items.WeaponItem> onEquipWeapon;
        //public event Action<WeaponItem> onEquipWearable;

        private List<Item> items;

        public int slotCount = 50;

        public List<Item> Items
        {
            get { return items; }
        }

        void Awake()
        {
            items = new List<Item>(new Item[slotCount]);
        }

        void Update()
        {
            if (System.GameController.Instance.GameState == System.GameStates.Game)
            {
                if (InputController.Item1) UseItem(0);
                if (InputController.Item2) UseItem(1);
                if (InputController.Item3) UseItem(2);
                if (InputController.Item4) UseItem(3);
                if (InputController.Item5) UseItem(4);
                if (InputController.Item6) UseItem(5);
            }
        }


        public bool AddItem(Item item)
        {
            int index = items.FindIndex(i => i == null);
            if (index == -1) return false;
            items[index] = item;
            return true;
        }

        public void UseItem(int slotId)
        {
            if (slotId < 0 || slotId > items.Count - 1) return;
            Item item = items[slotId];
            if (item == null || item.itemType == Item.ItemTypes.Quest) return;
            items[slotId] = null;
            item.Activate();
        }

        public void Move(int firstSlot, int secondSlot)
        {
            if (firstSlot >= Items.Count || secondSlot >= Items.Count || firstSlot < 0 || secondSlot < 0) return;
            Item item = items[firstSlot];
            items[firstSlot] = items[secondSlot];
            items[secondSlot] = item;
        }
    }
}
