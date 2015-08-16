using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FPSRPGPrototype.Combat
{
    public class AttackInformation
    {
        public enum AttackSources
        {
            None = 0,
            Player = 1,
            Enemy = 2,
            Enviroment = 3
        }

        public DamageConfig damage;
        public Vector3 point;
        public bool isShowEffect = true;
        public AttackSources source;

        public AttackInformation() { }

        public AttackInformation(DamageConfig damage, Vector3 point, bool isShowEffect, AttackSources source)
        {
            this.damage = damage;
            this.point = point;
            this.isShowEffect = isShowEffect;
            this.source = source;
        }
    }
}
