using UnityEngine.Networking;
using UnityEngine;

namespace FPSRPGPrototype.Networking
{
    public class PlayerInitilization : NetworkBehaviour
    {
        BaseClasses.Player player;


        void Start()
        {
            if (isLocalPlayer)
            {
                AttachPlayerScripts();
            }
        }

        void AttachPlayerScripts()
        {
            player = GetComponent<BaseClasses.Player>();

            if (player != null)
            {
                player.enabled = true;
                player.NetworkInitialize();
            }
        }
    }
}
