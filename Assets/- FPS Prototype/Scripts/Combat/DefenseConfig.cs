using System;
using UnityEngine;

namespace FPSRPGPrototype.Combat
{
    [Serializable]
    public class DefenseConfig
    {
        [Range(0f, 1f)]
        public float physical = 0;
        [Range(0f, 1f)]
        public float fire = 0;
        [Range(0f, 1f)]
        public float ice = 0;
        [Range(0f, 1f)]
        public float electro = 0;

        public int CalculateFinalDamage(DamageConfig damage, bool fromPlayer = false)
        {
            int final = 0;
            final = (int)(UnityEngine.Random.Range(damage.minPhysical, damage.maxPhysical) * (1f - Mathf.Clamp(physical, 0f, 1f)));
            final += (int)(damage.fire * (1f - Mathf.Clamp(fire, 0f, 1f)));
            final += (int)(damage.ice * (1f - Mathf.Clamp(ice, 0f, 1f)));
            final += (int)(damage.electro * (1f - Mathf.Clamp(electro, 0f, 1f)));
            return final;
        }
    }
}
