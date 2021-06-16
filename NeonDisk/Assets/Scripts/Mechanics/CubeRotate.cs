using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SelectionBase]
public class CubeRotate : MonoBehaviour
{
    public float mult = 3;

    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("NPC"))
        {
            _transform.Rotate(0.1f * mult, 0.01f * mult, 0.1f * mult, Space.World);
        }
        else
        {
            _transform.Rotate(0.01f * mult, 0.01f * mult, 0.01f * mult, Space.World);
        }
    }
}
