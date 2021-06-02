using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Disk Controller component that adds functionality to the disk object
/// Can be attacted to an object that uses the XRGrabInteractable class
/// </summary>
[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("XR/Disk Controller")]
public class DiskController : MonoBehaviour
{

    [ContextMenu("Help")]
    void Help(){
        Debug.Log("todo : add link to local html file that shows how to use this module");
    }

    private Rigidbody rb = null;
    private float _throwTime = 0;
    private bool m_isStuck = false;
    private int m_countedBounces = 0;

    [System.Serializable]
    public struct rb_constraints
    {
        public bool x;
        public bool y;
        public bool z;
    }


    [Header("Spawn Settings")]

    [SerializeField]
    [Tooltip("Spawns the object at the start pos")]
    bool respawnUsingStartPos = true;

    [SerializeField]
    [Tooltip("Transform of spawn object for Disk object")]
    Transform respawnTransform;

    /// <summary>
    /// Transform of object that the disk object spawns on
    /// </summary>
    public Transform RespawnTransform
    {
        get => respawnTransform;
        set => respawnTransform = value;
    }

    Vector3 startPos;

    /// <summary>
    /// The starting position of the disk object, if respawnUsingStartPos is true, it will use start pos otherwise it will use te respawn transform.
    /// </summary>
    public Vector3 StartPos
    {
        get => (respawnUsingStartPos ? startPos : respawnTransform.position);
        set => startPos = value;
    }


    [Header("Physics Settings")]

    [SerializeField]
    [Tooltip("What axis the object should not rotate on after released/dropped")]
    rb_constraints rotationConstraints;

    /// <summary>
    /// Locks the rotation of a specific axis in the rigidbody component that is attacted to the disk object
    /// </summary>
    public rb_constraints RotationConstraints
    {
        get => rotationConstraints;
        set => rotationConstraints = value;
    }

    [SerializeField]
    [Tooltip("Curve Smoothing for 'Cork-Screw effect'")]
    [Range(0,10)]
    float curveSmoothing;

    /// <summary> 
    /// Sets the smoothing amount on the disks curve rotation
    /// </summary>
    public float CurveSmoothing
    {
        get => curveSmoothing;
        set => curveSmoothing = value;
    }

    [SerializeField]
    [Tooltip("Amount of times the disk can bounce off other objects | 0 = infinite bounces")]
    int maxBounces = 0;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartPos = transform.position;
    }


    void Update()
    {
        if(maxBounces != 0 && m_countedBounces > maxBounces)
        {
            transform.position = StartPos;
            rb.velocity = Vector3.zero;
        }

        if (m_isStuck == true)
        {
            rb.isKinematic = true;
        }
        if (transform.position != StartPos && Vector3.Distance(Camera.main.transform.position, transform.position) > 3)
        {
            if (_throwTime == 0)
            {
                _throwTime = Time.realtimeSinceStartup + 10;
            }

            if (_throwTime < Time.realtimeSinceStartup)
            {
                if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                _throwTime = 0;
                transform.position = StartPos;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bouncabble"))
        {
            m_countedBounces +=1;
        }
        if(collision.gameObject.CompareTag("NonBounce"))
        {
            m_isStuck = true;
        }

    }

    public void DiskStuck()
    {
        rb.isKinematic = true;
    }
    public void DiskUnstuck()
    {
        rb.isKinematic = false;
    }

    public void UnlockConstraints()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.None;
    }

    public void LockConstraints()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (rotationConstraints.x == true)
        {
            body.constraints = RigidbodyConstraints.FreezeRotationX;
        }
        if (rotationConstraints.y == true)
        {
            body.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        if (rotationConstraints.z == true)
        {
            body.constraints = RigidbodyConstraints.FreezeRotationZ;
        }

    }
}
