﻿using System;
using System.Collections.Generic;
using FPSRPGPrototype.BaseClasses;
using UnityEngine;

namespace FPSRPGPrototype.Items
{
    public class InventoryController
    {
        public event Action<WeaponItem> onEquipWeapon;
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
