using UnityEngine;
using System;

namespace FPSRPGPrototype.System
{
    public enum GameStates
    {
        Game = 1,
        Inventory = 2,
        Finish = 3,
        Dialog = 4
    }

    public class GameController : Utilities.Singleton<GameController>
    {
        public event Action<GameStates> onStateChanged;
        public GameObject respawnPoint;

        private GameStates gameState;

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
            if (Player.InputController.Escape)
            {
                if (GameState == GameStates.Game)
                {
                    GameState = GameStates.Inventory;
                }
                else if (GameState == GameStates.Inventory)
                {
                    GameState = GameStates.Game;
                }
            }

            //if (InputController.Inventory)
            //{
            //    if (GameState == GameStates.Game)
            //    {
            //        GameState = GameStates.Inventory;
            //    }
            //    else if (GameState == GameStates.Inventory)
            //    {
            //        GameState = GameStates.Game;
            //    }
            //}
        }

        public void RestartGame()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

