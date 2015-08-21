using UnityEngine;
using System.Collections;

namespace FPSRPGPrototype.UI.MainMenu
{
    public class Connect : MonoBehaviour
    {
        public void ChangeToScene(int sceneTo)
        {
            Application.LoadLevel(sceneTo);
        }
    }
}

