/*
* File:			HandUI.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		04/06/2021
*	Last:		18/06/2021
* Summary:
*	Used to render the Hand UI on the left hand.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    public float updateTimer = 0;
    public Vector3 posOffset;
    public Vector3 angOffset;

    public GameObject[] disables;
    public Transform attachTransform;
    public Transform handTransform;

    private bool completed;

    void Start()
    {
        StartCoroutine(TestCast());
    }

    IEnumerator TestCast()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateTimer);

            if (!completed)
            {
                transform.SetParent(handTransform);

                transform.localPosition = posOffset;
                transform.localEulerAngles = angOffset;

                completed = true;
            }

            LayerMask mask = LayerMask.GetMask("InteractionUI");
            Vector3 dir = (attachTransform.position - Camera.main.transform.position).normalized;

            bool canSee = Physics.Raycast(attachTransform.position, dir, Mathf.Infinity, mask);

            foreach (GameObject disable in disables)
            {
                disable.SetActive(canSee);
            }
        }
    }

    /// <summary>
    /// Resets the current level.
    /// </summary>
    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        //foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Disk"))
        //{
        //    if (obj.TryGetComponent(out DiskController disk))
        //    {
        //        obj.transform.position = disk.StartPos;

        //        if (obj.TryGetComponent(out Rigidbody rb))
        //        {
        //            rb.velocity = Vector3.zero;
        //        }
        //    }
        //}
    }

    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
