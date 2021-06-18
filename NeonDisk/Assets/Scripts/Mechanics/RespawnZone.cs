/*
* File:			RespawnZone.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		2/05/2021
*	Last:		18/06/2021
* Summary:
*	Used for respawning the disk on trigger.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Disk")
            return;

        if (other.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (other.transform.TryGetComponent<DiskController>(out DiskController dc))
            other.transform.position = dc.StartPos;
    }
}
