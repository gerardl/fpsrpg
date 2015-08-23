using UnityEngine;
using FPSRPGPrototype.System;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPSRPGPrototype.Player
{
    public class InputController : MonoBehaviour
    {
        public enum MenuStates
        {
            Game = 1,
            Inventory = 2,
            Dialog = 3
        }

        private MenuStates menuState;

        FirstPersonController fpsController;

        public static float ForwardBack { get; private set; }
        public static float LeftRight { get; private set; }
        public static float Horizontal { get; private set; }
        public static float Vertical { get; private set; }

        public static bool Weapon { get; private set; }
        public static bool Escape { get; private set; }
        public static bool Inventory { get; private set; }

        public static bool Item1 { get; private set; }
        public static bool Item2 { get; private set; }
        public static bool Item3 { get; private set; }
        public static bool Item4 { get; private set; }
        public static bool Item5 { get; private set; }
        public static bool Item6 { get; private set; }

        private static bool use;

        public static bool Use
        {
            get
            {
                bool b = use;
                use = false;
                return b;
            }
        }

        void Awake()
        {
            fpsController = (FirstPersonController)gameObject.GetComponent(typeof(FirstPersonController));
            menuState = MenuStates.Game;
        }
        

        void Update()
        {
            Escape = Input.GetKeyDown(KeyCode.Escape);

            // The FPS Controller will handle movement for now,
            // I am leaving this here because we may want to implement
            // "combos" or the like and would want to know if these are being
            // pressed.
            //
            //ForwardBack = 0;
            //LeftRight = 0;
            //if (Input.GetKey(KeyCode.W)) ForwardBack = 1;
            //if (Input.GetKey(KeyCode.S)) ForwardBack = -1;
            //if (Input.GetKey(KeyCode.A)) LeftRight = -1;
            //if (Input.GetKey(KeyCode.D)) LeftRight = 1;
            //Horizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
            //Vertical = -Input.GetAxis("Mouse Y") * mouseSensitivity;

            if (menuState == MenuStates.Game)
            {
                fpsController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;

                use = Input.GetKeyDown(KeyCode.E);
                Weapon = Input.GetKey(KeyCode.Mouse0);
                Inventory = Input.GetKeyDown(KeyCode.I);

                Item1 = Input.GetKeyDown(KeyCode.Alpha1);
                Item2 = Input.GetKeyDown(KeyCode.Alpha2);
                Item3 = Input.GetKeyDown(KeyCode.Alpha3);
                Item4 = Input.GetKeyDown(KeyCode.Alpha4);
                Item5 = Input.GetKeyDown(KeyCode.Alpha5);
                Item6 = Input.GetKeyDown(KeyCode.Alpha6);
            }
            else if (menuState == MenuStates.Inventory)
            {
                fpsController.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            // From GameController, needs to be refactored here (probably)
            if (Escape)
            {
                if (menuState == MenuStates.Game)
                {
                    menuState = MenuStates.Inventory;
                }
                else if (menuState == MenuStates.Inventory)
                {
                    menuState = MenuStates.Game;
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
    }
}

