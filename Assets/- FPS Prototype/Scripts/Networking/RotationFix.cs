using UnityEngine;
using UnityEngine.Networking;

namespace FPSRPGPrototype.Networking
{
    // this is a workaround for a bug in NetworkTransform in SyncCharacterController mode
    public class RotationFix : MonoBehaviour
    {
        Vector3 oldPostion = Vector3.zero;
        Quaternion oldRotation = Quaternion.identity;

        void Update()
        {
            if (!NetworkClient.active)
                return;

            if (transform.position != oldPostion)
            {
                oldPostion = transform.position;
                return;
            }

            var newRotation = transform.rotation;
            var diff = Quaternion.Angle(newRotation, oldRotation);
            if (diff > 0)
            {
                // position is the same but rotation has changed
                oldRotation = newRotation;

                // move a little so transform is set dirty
                transform.position += new Vector3(0, 0, 0.00001f);
            }
        }
    }
}


