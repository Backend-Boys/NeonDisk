/*
* File:			WinGame.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		17/06/2021
*	Last:		18/06/2021
* Summary:
*	Used for fading out the scene and sending win information after scoring a goal (the trigger).
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public AudioSource winSound;
    public bool win = false;
    public GameObject menu;
    public UnityEngine.UI.Text text;

    private MeshRenderer[] meshRenders;

    private void Awake()
    {
        meshRenders = (MeshRenderer[])GameObject.FindObjectsOfType(typeof(MeshRenderer));
    }

    void Update()
    {
        if (win)
        {
            foreach (MeshRenderer obj in meshRenders)
            {
                obj.material.SetFloat("EmissionStrength", Mathf.Lerp(obj.material.GetFloat("EmissionStrength"), 0, Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        win = true;
        menu.SetActive(true);

        //Scoring.main.AddPoints();
        Scoring.main.Goal();

        text.text = $"Score: {Scoring.main.playerScore} / {Scoring.main.maxScore}";
        //

        Destroy(other.gameObject);
        //winSound.Play();
    }
}
