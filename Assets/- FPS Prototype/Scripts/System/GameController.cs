using UnityEngine;
using System;

namespace FPSRPGPrototype.System
{
    public enum GameStates
    {
        Game = 1,
        Inventory = 2,
        Pause = 3,
        Finish = 4,
        Dialog = 5
    }

    public class GameController : Utilities.Singleton<GameController>
    {
        public event Action<GameStates> onStateChanged;

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
            GameState = GameStates.Pause;
        }

        private void OnStateChanged(GameStates state)
        {
            Time.timeScale = state == GameStates.Pause || state == GameStates.Inventory ? 0 : 1;
        }

        void Update()
        {
            if (InputController.Escape)
            {
                if (GameState == GameStates.Game)
                {
                    GameState = GameStates.Pause;
                }
                else if (GameState == GameStates.Pause)
                {
                    GameState = GameStates.Game;
                }
            }

            if (InputController.Inventory)
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
        }

        public void RestartGame()
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

