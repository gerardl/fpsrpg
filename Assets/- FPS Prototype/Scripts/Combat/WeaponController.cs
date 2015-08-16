using UnityEngine;

namespace FPSRPGPrototype.Combat
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject weaponHand;
        private Items.WeaponItem weapon;
        public Items.WeaponItemRanged tempRangedWeapon;
        private Animator animator;
        private GameObject weaponModel;
        private System.InputController inputController;

        void Awake()
        {
            animator = GetComponent<Animator>();
            
        }

        void Update()
        {
            //if (GameController.Instance.GameState != GameStates.Game) return;
            //animator.SetBool("IsAttack", System.InputController.Weapon);
            if (System.InputController.Weapon) OnAttack();
        }

        // by the animation event
        public void OnAttack()
        {
            var player = GetComponentInParent<BaseClasses.Player>();

            var playerPosition = player.fpsCamera.transform.position;
            playerPosition.x += 1;
            playerPosition.y += 1;
            //playerPosition.z += 1;

            Instantiate(tempRangedWeapon.projectile, playerPosition, player.fpsCamera.transform.rotation);

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
                    player.Mana -= rangedWeapon.mana;

                    break;
                case BaseClasses.Item.ItemTypes.Wearable:
                    break;
                default:
                    break;
            }
        }
    }
}
