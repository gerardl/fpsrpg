using UnityEngine;
using System;
using FPSRPGPrototype.System;
using FPSRPGPrototype.Interfaces;
using UnityEngine.Networking;

namespace FPSRPGPrototype.BaseClasses
{
    public class Player : NetworkBehaviour, IKillable, INetworkable
    {
        #region Properties

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
        public Combat.WeaponController weaponController;
        public Items.InventoryController inventoryController;
        public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
        public Camera fpsCamera;
        public AudioListener fpsCameraAudioListender;
        public AudioSource audioSource;

        public IInteractive InteractiveObject { get; private set; }

        //public int MaxHealth
        //{
        //    get { return maxHealth; }
        //    set
        //    {
        //        maxHealth = value;

        //        if (Health > MaxHealth)
        //            Health = MaxHealth;
        //    }
        //}

        //public int MaxMana
        //{
        //    get { return maxMana; }
        //    set
        //    {
        //        maxMana = value;

        //        if (Mana > MaxMana)
        //            Mana = MaxMana;
        //    }
        //}

        //public int Health
        //{
        //    get { return health; }
        //    set
        //    {
        //        health = Mathf.Clamp(value, 0, maxHealth);

        //        if (health == 0)
        //        {
        //            KillPlayer();
        //        }

        //    }
        //}

        //public int Mana
        //{
        //    get { return mana; }
        //    set
        //    {
        //        mana = Mathf.Clamp(value, 0, maxMana);
        //    }
        //}

        #endregion

        #region Unity Methods

        public override void OnStartLocalPlayer()
        {
            this.NetworkInitialize();
        }

        void Awake()
        {
            // need to replace with persisted health (obv ;))
            maxHealth = testingHealth;
            health = testingHealth;
            maxMana = testingHealth;
            mana = testingMana;

           characterController = GetComponent<CharacterController>();

            if (characterController != null)
                Debug.Log("found character");
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
                InteractiveObject.Use();
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

        // originally from unity, can't find the link, is here as well as a few other spots:
        //https://github.com/kurilchenko/1look/blob/master/Assets/Scripts/MouseLook.cs
        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, -75, 75);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        private void KillPlayer()
        {
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            Debug.Log("I'm dead now");
            characterController.SimpleMove(new Vector3(200, 200, 200));
        }

        public void Attack(Combat.AttackInformation attackInformation)
        {
            //if (!isServer)
            //    return;
            
            //SoundController.Play("hit_player");
            int damage = defense.CalculateFinalDamage(attackInformation.damage);
            //HitTime = Time.time;
            Debug.Log("in Attack() on Player");
            // I can't use the property here, because apparently it will not get marked
            // as dirty.  Seems maybe I can't use properties much because of this?
            health -= damage;

            health = Mathf.Clamp(health, 0, maxHealth);

            if (health == 0)
            {
                KillPlayer();
            }
        }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void NetworkInitialize()
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


