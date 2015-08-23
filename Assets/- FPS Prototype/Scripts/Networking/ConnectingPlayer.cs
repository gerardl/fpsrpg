using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FPSRPGPrototype.Networking
{
    public class ConnectingPlayer : NetworkLobbyPlayer
    {
        public string playerName { get; set; }
    }
}
