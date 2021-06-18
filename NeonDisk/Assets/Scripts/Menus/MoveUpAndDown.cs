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
