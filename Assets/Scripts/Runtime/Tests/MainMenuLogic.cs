using UnityEngine;
using Z3.UIBuilder.Core;

namespace Z3.GMTK2024
{
    public class MainMenuLogic : MonoBehaviour 
    {
        public void CloseGame()
        {
            Debug.Log("Closing the app, please wait...");
            Application.Quit();
        }
    }
}