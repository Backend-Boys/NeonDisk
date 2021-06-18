/*
* File:			Elevator.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		17/06/2021
*	Last:		18/06/2021
* Summary:
*	Used for loading and making a cool scene transition.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeonDiskVR.Menus
{
    public class Elevator : MonoBehaviour
    {
        public static string nextScene = "Level 1";

        public AudioSource landSound;

        public Transform player;
        public Transform leftDoor;
        public Transform rightDoor;

        private bool openDoors = false;

        void Start()
        {
            StartCoroutine(Wait(5));
        }

        private IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            player.gameObject.SetActive(false);

            yield return SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));

            landSound.Play();

            yield return new WaitForSeconds(landSound.clip.length / 2);

            openDoors = true;

            yield return new WaitForSeconds(1.5f);

            SceneManager.UnloadSceneAsync("Elevator");

            yield return SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        }

        void Update()
        {
            if (openDoors)
            {
                leftDoor.position -= leftDoor.right * Time.deltaTime;
                rightDoor.position += rightDoor.right * Time.deltaTime;
            }
        }
    }
}