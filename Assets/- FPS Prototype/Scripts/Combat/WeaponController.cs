using UnityEngine;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Combat
{
    public class WeaponController : NetworkBehaviour
    {
        public GameObject weaponHand;
        private Items.WeaponItem weapon;
        public Items.WeaponItemRanged tempRangedWeapon;
        private BaseClasses.Player player;
        private Animator animator;
        private GameObject weaponModel;
        private System.InputController inputController;

        public float castDelay = .66f;
        private float lastCast;

        void Awake()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<BaseClasses.Player>();
        }

        void Update()
        {
            if (!isLocalPlayer)
                return;

            //if (GameController.Instance.GameState != GameStates.Game) return;
            //animator.SetBool("IsAttack", System.InputController.Weapon);
            if (System.InputController.Weapon && Time.time >= lastCast)
            {
                lastCast = Time.time + castDelay;
                CmdShootProjectile();
            }
            //OnAttack();
                
        }

        // by the animation event
        public void OnAttack()
        {
            CmdShootProjectile();


            if (weapon == null) return;
            
            switch (weapon.itemType)
            {
                case BaseClasses.Item.ItemTypes.Quest:
                    break;
                case BaseClasses.Item.ItemTypes.Potion:
                    break;
                case BaseClasses.Item.ItemTypes.Food:
                    break;
                case BaseClasses.Item.ItemTypes.MeleeWeapon:
                    break;
                case BaseClasses.Item.ItemTypes.RangedWeapon:

                    var rangedWeapon = (Items.WeaponItemRanged)weapon;

                    Instantiate(rangedWeapon.projectile, player.fpsCamera.transform.position, player.fpsCamera.transform.rotation);
                    //player.Mana -= rangedWeapon.mana;

                    break;
                case BaseClasses.Item.ItemTypes.Wearable:
                    break;
                default:
                    break;
            }
        }

        [Command]
        private void CmdShootProjectile()
        {
            RpcShootProjectile();
        }

        [ClientRpc]
        private void RpcShootProjectile()
        {
            //var playerPosition = player.fpsCamera.transform.position;
            var playerPosition = player.gameObject.transform.position;
            playerPosition.x += 1;
            playerPosition.y += 1;
            //playerPosition.z += 1;

            Instantiate(tempRangedWeapon.projectile, playerPosition, player.fpsCamera.transform.rotation);
            
        }
    }
}
