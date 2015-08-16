using UnityEngine.Networking;
using UnityEngine;

namespace FPSRPGPrototype.Networking
{
    public class PlayerInitilization : NetworkBehaviour
    {
        void Start()
        {
            if (isLocalPlayer)
            {
                AttachPlayerScripts();
            }
        }

        void AttachPlayerScripts()
        {
            var player = GetComponent<BaseClasses.Player>();

            player.enabled = true;
            player.NetworkInitialize();
        }
    }
}
