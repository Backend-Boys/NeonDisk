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

