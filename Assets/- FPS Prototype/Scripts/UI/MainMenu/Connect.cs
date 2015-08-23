using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace FPSRPGPrototype.UI.MainMenu
{
    public class Connect : MonoBehaviour
    {
        public void ShowGUI()
        {
            
        }

        public void ChangeToScene(int sceneTo)
        {

            Application.LoadLevel(sceneTo);
        }
    }
}

