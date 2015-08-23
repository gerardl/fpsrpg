using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Networking
{
    public class CustomLobbyHook : LobbyHook
    {
        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            ConnectingPlayer connectingPlayer = lobbyPlayer.GetComponent<ConnectingPlayer>();

            
        }
    }
}
