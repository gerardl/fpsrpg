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

        [Command]
        private void CmdPickUpDrop()
        {
            RpcPickUpDrop();
        }

        [ClientRpc]
        private void RpcPickUpDrop()
        {
            Debug.Log("Tried To use this");
            Destroy(gameObject);
        }


        public void Use()
        {
            CmdPickUpDrop();
        }
    }
}
