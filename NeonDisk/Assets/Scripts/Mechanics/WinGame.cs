using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public AudioSource winSound;
    public bool win = false;
    public GameObject menu;

    void Update()
    {
        if (win)
        {
            foreach (MeshRenderer obj in GameObject.FindObjectsOfType(typeof(MeshRenderer)))
            {
                obj.material.SetFloat("EmissionStrength", Mathf.Lerp(obj.material.GetFloat("EmissionStrength"), 0, Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        win = true;
        menu.SetActive(true);
        //winSound.Play();
    }
}
