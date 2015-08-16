using UnityEngine;
using FPSRPGPrototype.Interfaces;

namespace FPSRPGPrototype.Combat
{
    public class Projectile : MonoBehaviour
    {
        public DamageConfig damage;
        public float speed = 10;
        public GameObject corpse;
        public AttackInformation.AttackSources attackSource;

        void Start()
        {
            Destroy(gameObject, 5f);
        }

        void Update()
        {
            Move();
            CheckHit();
        }

        protected virtual void Move()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }

        protected virtual void CheckHit()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, Utilities.LayersHelper.DefaultEnemyPlayer))
            {
                Debug.Log("hit something");
                IKillable killable = hit.collider.gameObject.GetComponent(typeof(IKillable)) as IKillable;

                if (killable != null)
                {
                    killable.Attack(new AttackInformation(damage, hit.point, true, attackSource));
                }

                DestroyProjectile();
            }
        }

        protected virtual void DestroyProjectile()
        {
            if (corpse != null) Instantiate(corpse, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}