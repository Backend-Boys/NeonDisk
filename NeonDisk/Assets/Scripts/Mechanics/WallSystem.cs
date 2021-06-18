/*
* File:			RespawnZone.cs
* Author:		Duncan Sykes (s200490@students.aie.edu.au)
* Edit Dates:
*	First:		31/05/2021
*	Last:		18/06/2021
* Summary:
*	Used for respawning the disk on trigger.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[AddComponentMenu("NeonDisk/Wall Controller")]

public class WallSystem : MonoBehaviour
{

    [System.Serializable]
    public enum wallEvents
    {
        bounce,
        destruct,
        sticky
    };


    [Header("Wall Events")]
    public wallEvents wallEvent;
    
    public void RunFunction(DiskController diskController)
    {
        switch (wallEvent)
        {
            case wallEvents.bounce:
            {
                diskController.CountedBounces+=1;
                Debug.Log("Bounces: " + diskController.CountedBounces);
                break;
            }
            case wallEvents.destruct:
            {
                // To do 
                // Add code that makes the disk break / destruct on wall collision
                break;
            }
            case wallEvents.sticky:
            {
                diskController.IsStuck=true;
                Debug.Log("Stuck: " + diskController.IsStuck);
                break;
            }

            default:
                break;
        }
    }

    

    
}
