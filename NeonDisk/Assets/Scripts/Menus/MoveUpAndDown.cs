/*
* File:			MoveUpAndDown.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		17/06/2021
*	Last:		18/06/2021
* Summary:
*	Used for moving the light up and down in the elevator scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public float speed = 1f;
    public float moveMult = 0.05f;

    private bool up = true;
    private float move = -1; // Starts at the bottom

    void FixedUpdate()
    {

        if (up)
        {
            transform.position += transform.up * Time.deltaTime * speed;

            move += moveMult;
        }
        else
        {
            transform.position -= transform.up * Time.deltaTime * speed;

            move -= moveMult;
        }

        if (up && move >= 1)
        {
            up = false;
        }
        else if (!up && move <= -1)
        {
            up = true;
        }
    }
}
