using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System;
using System.Collections;
using UnityEngine.UI;

namespace FPSRPGPrototype.Chat
{
    public class ChatHub : NetworkBehaviour
    {
        // clean up all of these references when I know how best to
        // organize all of the ui / chat stuff
        // - glucas

        const short CHAT_MSG = MsgType.Highest + 1; // Unique message ID
        public Text chat;
        public InputField inputBox;
        public PlayerController _player;
        NetworkClient client;

        void Start()
        {
            NetworkManager netManager = FindObjectOfType<NetworkManager>();
            
            if (netManager == null) return;

            Debug.Log("found netManager");

            client = netManager.client;
            if (client.isConnected)
                client.RegisterHandler(CHAT_MSG, ClientReceiveChatMessage);
            if (isServer)
                NetworkServer.RegisterHandler(CHAT_MSG, ServerReceiveChatMessage);
        }

        public void SendChatMessage(string msg)
        {
            StringMessage strMsg = new StringMessage(msg);
            Debug.Log("input box:" + inputBox.text);

            if (isServer)
            {
                NetworkServer.SendToAll(CHAT_MSG, strMsg); // Send to all clients
                foreach (var a in NetworkServer.objects)
                {
                    Debug.Log("key: " + a.Key + " value: " + a.Value);
                }
                //NetworkServer.connections.ForEach(f => Debug.Log("connid: " + f.connectionId + " isReady: " + f.isReady));
            }
            else if (client.isConnected)
            {
                client.Send(CHAT_MSG, strMsg); // Sending message from client to server
                inputBox.text = string.Empty;
            }
        }

        public void ServerReceiveChatMessage(NetworkMessage netMsg)
        {
            string str = netMsg.ReadMessage<StringMessage>().value;
            if (isServer)
            {
                SendChatMessage(str); // Send the chat message to all clients
            }
        }

        public void ClientReceiveChatMessage(NetworkMessage netMsg)
        {
            string str = netMsg.ReadMessage<StringMessage>().value;
            if (client.isConnected)
            {
                Debug.Log(str);
                chat.text += Environment.NewLine + str; // Add the message to the client's local chat window
            }
        }

    }
}
