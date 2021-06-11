using UnityEngine;

namespace NeonDiskVR.Menus
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(string a_sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(a_sceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

