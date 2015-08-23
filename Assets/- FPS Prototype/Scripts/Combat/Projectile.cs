using UnityEngine;
using FPSRPGPrototype.Interfaces;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Combat
{
    [RequireComponent(typeof(NetworkTransform))]
    public class Projectile : NetworkBehaviour
    {
        public DamageConfig damage;
        public float speed = 10;
        public GameObject corpse;
        public AttackInformation.AttackSources attackSource;
        public Rigidbody _rigidbody;

        void Awake()
        {
            Collider c = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();

            c.material.frictionCombine = PhysicMaterialCombine.Minimum;
            c.material.dynamicFriction = 0.0f;
            c.material.staticFriction = 0.0f;
            _rigidbody.useGravity = false;
        }

        void Start()
        {
            //Destroy(gameObject, 5f);
        }

        void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            //rigidbody.isKinematic = false;
            //rigidbody.velocity = Vector3.zero;
            //rigidbody.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }

        [ServerCallback]
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("in OnCollisionEnter");
            //Debug.Log(collision);

            var killable1 = collision.collider.GetComponent(typeof(IKillable)) as IKillable;
            //var killable2 = collision.collider.gameObject.GetComponent(typeof(IKillable)) as IKillable;

            if (killable1 != null)
            {
                Debug.Log("in killable1");
                killable1.Attack(new AttackInformation(damage, collision.contacts[0].point, true, attackSource));
            }
            //if (killable2 != null)
            //{
            //    Debug.Log("in killable2");
            //    killable2.Attack(new AttackInformation(damage, collision.contacts[0].point, true, attackSource));
            //}

            DestroyProjectile();
        }

        protected virtual void DestroyProjectile()
        {
            if (corpse != null) Instantiate(corpse, transform.position, Quaternion.identity);
            Debug.Log("in destroy");
            Destroy(this.gameObject);
        }
    }
}