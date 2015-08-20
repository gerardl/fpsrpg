using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

namespace FPSRPGPrototype.Chat
{
    public class ChatHub : NetworkBehaviour
    {

        const short CHAT_MSG = MsgType.Highest + 1; // Unique message ID
        //public Chat chat; // Separate, non-networked script handling the chat window/interface/GUI
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
            if (isServer)
            {
                NetworkServer.SendToAll(CHAT_MSG, strMsg); // Send to all clients
            }
            else if (client.isConnected)
            {
                client.Send(CHAT_MSG, strMsg); // Sending message from client to server
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
                //chat.AppendMessage(str); // Add the message to the client's local chat window
            }
        }

    }
}
