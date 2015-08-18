using UnityEngine.Networking;
using UnityEngine;

namespace FPSRPGPrototype.Networking
{
    public class PlayerInitilization : NetworkBehaviour
    {
        BaseClasses.Player player;

        public override void OnStartLocalPlayer()
        {
            AttachPlayerScripts();
        }
        //void Start()
        //{
        //    if (isLocalPlayer)
        //    {
        //        AttachPlayerScripts();
        //    }
        //}

        void AttachPlayerScripts()
        {
            Debug.Log("in AttachPlayerScripts");
            
            player = GetComponent<BaseClasses.Player>();

            if (player != null)
            {
                //player.enabled = true;
                player.name = "im networkz";
                player.NetworkInitialize();
            }
        }
    }
}
