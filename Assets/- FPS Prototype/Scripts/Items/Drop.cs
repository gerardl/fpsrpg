using System;
using FPSRPGPrototype.Interfaces;
using UnityEngine;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Items
{
    public class Drop : NetworkBehaviour, IInteractive
    {
        public string itemId = "";
        public SpriteRenderer spriteRenderer;
        private BaseClasses.Item item;

        public string Name
        {
            get { return item.name; }
        }

        public bool IsActive
        {
            get { return true; }
        }

        public Actions Action
        {
            get { return Actions.Take; }
        }
        
        //[ClientRpc]
        //private void RpcPickUpDrop()
        //{
        //    Debug.Log("Tried To use this");
        //    Destroy(gameObject);
        //}

        public void Interact(BaseClasses.Player player)
        {
            Use(player);
        }

        public void Use(BaseClasses.Player player)
        {
            //if (!isLocalPlayer)
            //    return;
            player.Use(this.gameObject);
        }
    }
}
