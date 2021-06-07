using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(string a_sceneName)
    {
        SceneManager.LoadSceneAsync(a_sceneName);
    }
}
