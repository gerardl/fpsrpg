using System;
using FPSRPGPrototype.Interfaces;
using UnityEngine;

namespace FPSRPGPrototype.Items
{
    public class Drop : MonoBehaviour, Interfaces.IInteractive
    {
        public string itemId = "";
        public SpriteRenderer spriteRenderer;
        private BaseClasses.Item item;

        public string Name { get { return item.name; } }
        public bool IsActive { get { return true; } }
        public Actions Action { get { return Actions.Take; } }

        public void Use()
        {
            Debug.Log("Tried To use this");
        }
    }
}
