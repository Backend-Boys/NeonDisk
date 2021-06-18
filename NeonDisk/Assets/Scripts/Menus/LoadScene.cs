/*
* File:			LoadScene.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		04/06/2021
*	Last:		18/06/2021
* Summary:
*	Loading a scene with possibly an elevator.
*/

using UnityEngine;

namespace NeonDiskVR.Menus
{
    public class LoadScene : MonoBehaviour
    {
        public bool useElevator = false;

        public void Load(string a_sceneName)
        {
            if (useElevator)
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Elevator");
                Elevator.nextScene = a_sceneName;
            }
            else
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(a_sceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

