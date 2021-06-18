/*
* File:			spinmesh.cs
* Author:		Duncan Sykes (s200490@students.aie.edu.au)
* Edit Dates:
*	First:		4/06/2021
*	Last:		18/06/2021
* Summary:
*	Used to spin the disk.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinmesh : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform m_transform;

    public bool spinning = false;
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spinning)
            m_transform.Rotate(new Vector3(0,0.5f,0), Space.World);
        
    }
}
