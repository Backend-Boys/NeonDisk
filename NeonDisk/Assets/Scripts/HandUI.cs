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
    /// Reset the disks positions to their default state. Should add a reset function inside of disk.
    /// </summary>
    public void ResetLevel()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Disk"))
        {
            if (obj.TryGetComponent(out DiskController disk))
            {
                obj.transform.position = disk.StartPos;

                if (obj.TryGetComponent(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
