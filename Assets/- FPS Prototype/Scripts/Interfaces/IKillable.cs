using UnityEngine;
using System.Collections;

namespace FPSRPGPrototype.Interfaces
{
    public interface IKillable
    {
        void Attack(Combat.AttackInformation attackInformation);

        void Kill();
    }
}
