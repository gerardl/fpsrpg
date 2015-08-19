using UnityEngine;
using System;
using FPSRPGPrototype.System;
using FPSRPGPrototype.Interfaces;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Player
{
    // Central location to control playable character.
    // most functions run on the server from the client 
    // are called through this or a child controller
    //
    // scripts in this folder are components on the player prefab
    public class PlayerController : NetworkBehaviour, IKillable
    {
        #region Properties

        // Note:  Cannot do normal getters and setters due to 
        //        SyncVar.  It will not mark a property as dirty if
        //        updated indirectly through a get or set.

        [SyncVar]
        public string playerName;

        public int testingHealth = 100;
        public int testingMana = 100;

        public float speed = 10f;
        public float rotationSpeed = 10f;

        [SyncVar]
        private int maxHealth;
        [SyncVar]
        private int maxMana;
        [SyncVar]
        private int health;
        [SyncVar]
        private int mana;

        public CharacterController characterController;
        public Combat.DefenseConfig defense;
        public WeaponController weaponController;
        public InventoryController inventoryController;
        public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
        public Camera fpsCamera;
        public AudioListener fpsCameraAudioListender;
        public AudioSource audioSource;

        public IInteractive InteractiveObject { get; private set; }

        
        #endregion

        #region Unity Methods

        public override void OnStartLocalPlayer()
        {
            this.NetworkInitialize();
        }

        void Awake()
        {
            maxHealth = testingHealth;
            health = testingHealth;
            maxMana = testingHealth;
            mana = testingMana;

            characterController = GetComponent<CharacterController>();

            if (characterController != null)
                Debug.Log("found character");
        }
        
        // temp 
        void OnGUI()
        {
            if (!isLocalPlayer)
                return;
            
            // temp: draw a crosshair.
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");

            // temp: hp bar
            GUI.Box(new Rect(20, 20, 100, 20), "hp: " + health.ToString() + " / " + maxHealth.ToString());
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer)
                return;

            if (GameController.Instance.GameState != GameStates.Game)
                return;

            // Search for interactive objects
            InteractiveObject = null;

            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, 4))
            {
                InteractiveObject = hit.collider.gameObject.GetComponent(typeof(IInteractive)) as IInteractive;

                if (InteractiveObject != null && InteractiveObject.IsActive == false)
                    InteractiveObject = null;
            }

            if ((InteractiveObject != null) && (InputController.Use))
            {
                InteractiveObject.Interact(this);
            }

            // Mana regeneration;
            //if (Time.time - manaRegenerationTime > baseManaRegeenrationDeleay - (baseManaRegeenrationDeleay * 0.05 * experience.Mind))
            //{
            //    Mana++;
            //    manaRegenerationTime = Time.time;
            //}
        }

        #endregion

        #region Class Methods

        private void KillPlayer()
        {
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
        }


        public void Attack(Combat.AttackInformation attackInformation)
        {
            if (!isServer)
                return;

            //SoundController.Play("hit_player");
            Debug.Log("in Attack() on Player");

            int damage = defense.CalculateFinalDamage(attackInformation.damage);
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);

            if (health == 0)
                KillPlayer();
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void Use(GameObject gameObject)
        {
            CmdPickUpDrop(gameObject);
        }

        [Command]
        private void CmdPickUpDrop(GameObject gameObject)
        {
            Debug.Log("Tried To use this");
            NetworkServer.Destroy(gameObject);
        }

        private void NetworkInitialize()
        {
            Debug.Log("begin initilization");
            this.name = "in network init";
            this.playerName = "test in init";
            //characterController.enabled = true;
            fpsCamera.enabled = true;
            fpsCameraAudioListender.enabled = true;
            fpsController.enabled = true;
            //weaponController.enabled = true;

            Debug.Log("completed network initilization");
        }

        #endregion
    }
}


