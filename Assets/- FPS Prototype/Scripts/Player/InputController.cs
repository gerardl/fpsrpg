using UnityEngine;
using FPSRPGPrototype.System;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPSRPGPrototype.Player
{
    public class InputController : MonoBehaviour
    {
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

            if (GameController.Instance.GameState == GameStates.Game)
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
            else if (GameController.Instance.GameState == GameStates.Inventory)
            {
                fpsController.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}

