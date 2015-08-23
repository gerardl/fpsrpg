using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

namespace FPSRPGPrototype.System
{
    public enum GameStates
    {
        Game = 1,
        Finish = 2
    }

    public class GameController : NetworkBehaviour//, Utilities.Singleton<GameController>
    {
        static public GameController Instance { get { return _instance; } }
        static protected GameController _instance;

        public event Action<GameStates> onStateChanged;
        public GameObject respawnPoint;

        private GameStates gameState;

        public List<Player.PlayerController> connectedPlayers;

        public GameStates GameState
        {
            get { return gameState; }
            set
            {
                if (value == gameState) return;
                gameState = value;
                if (onStateChanged != null) onStateChanged(gameState);
            }
        }

        void Awake()
        {
            onStateChanged += OnStateChanged;

            connectedPlayers = new List<Player.PlayerController>();

            _instance = this;
        }

        void Start()
        {
            GameState = GameStates.Game;
        }

        private void OnStateChanged(GameStates state)
        {
            //Time.timeScale = state == GameStates.Pause || state == GameStates.Inventory ? 0 : 1;
        }

        void Update()
        {
            
        }

        public void RestartGame()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

