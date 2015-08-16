using UnityEngine;

namespace FPSRPGPrototype.Utilities
{
    // generic singleton of types deriving from monobehaviour.
    // seems like in unity you need to get an isntance of a gameobject from
    // an entire scene (findObjectOfType) often, and it is really slow.  some info:
    // http://unitypatterns.com/singletons/
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null || instance.gameObject == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                        Debug.LogWarning("Could not get an instance of" + typeof(T));
                }

                return instance;
            }
        }
    }
}

