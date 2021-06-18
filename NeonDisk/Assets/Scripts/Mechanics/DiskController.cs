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
[AddComponentMenu("NeonDisk/Disk Controller")]
public class DiskController : MonoBehaviour
{

    [ContextMenu("Help")]
    void Help()
    {
        Debug.Log("todo : add link to local html file that shows how to use this module");
    }


    public NPC test;
    [Header("Sounds")]
    public AudioSource hitSound;
    public AudioSource killSound;

    private Vector3 centreOfMass;
    [Header("Number of frames to track flick")]
    [SerializeField]
    public Vector3[] velocityFrames;
    public Vector3[] angularVelocity;
    private int frameStep = 0;

    private Rigidbody rb = null;
    private float _throwTime = 0;
    private bool m_isStuck = false;

    public bool IsStuck
    {
        get => m_isStuck;
        set => m_isStuck = value;
    }

    private int m_countedBounces = 0;

    public int CountedBounces
    {
        get => m_countedBounces;
        set => m_countedBounces = value;
    }


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
    [Tooltip("Curve Amount")]
    [Range(0, 1)]
    float curveSmoothing;

    /// <summary> 
    /// Sets the smoothing amount on the disks curve rotation
    /// </summary>
    public float CurveAmount
    {
        get => curveSmoothing;
        set => curveSmoothing = value;
    }

    public bool velocityAffectsCurve = false;

    [SerializeField]
    [Tooltip("Amount of times the disk can bounce off other objects | 0 = infinite bounces")]
    int maxBounces = 0;

    public void Reset()
    {
        m_countedBounces = 0;
        this.transform.position = StartPos;
        rb.velocity = Vector3.zero;
        
        rb.angularVelocity = Vector3.zero;
        m_isStuck = false;

        Scoring.main.AddThrow();
    }

    private void DebugTest()
    {
       
         
        
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        StartPos = transform.position;
        centreOfMass = rb.centerOfMass;
    }

    void FixedUpdate()
    {
        DebugTest();
        TimerCheck();
        
        VelocityUpdate();
        
        if (!velocityAffectsCurve)
        {
            // Calculate the disk curve amount
            Vector3 sideDir = Vector3.Cross(transform.up, rb.velocity).normalized;
            rb.AddForce(sideDir * curveSmoothing);
        }
        else
        {

            Vector3 sideDir = Vector3.Cross(transform.up, rb.velocity).normalized;
            rb.AddForce(sideDir * curveSmoothing * (rb.velocity.magnitude * 10));

        }
    }

    void VelocityUpdate()
    {
        if (velocityFrames != null)
        {
            frameStep++;
            if (frameStep >= velocityFrames.Length)
            {   
                frameStep =0;
            }

            velocityFrames[frameStep] = rb.velocity;
            angularVelocity[frameStep] = rb.angularVelocity;
        }
    }

    void AddVelocityHistory()
    {
        if (velocityFrames !=null)
        {
            // Get the average vector from last 5 frames
            Vector3 velocityAverage = GetVectorAverage(velocityFrames);
            if (velocityAverage != null)
            {
                // if our average isn't 0, apply it to the rigidbody
                rb.velocity = velocityAverage;
            }

            // do the same for angular velocity
            Vector3 angularVelocityAverage = GetVectorAverage(angularVelocity);
            if (angularVelocityAverage != null)
            {
                // if our average isn't 0, apply it to the rigidbody
                rb.angularVelocity = angularVelocityAverage;
            }
        }
    }

    void ResetVelocityHistory()
    {
        // reset the current frame step to 0
        frameStep = 0;
        // prevent nulls
        if (velocityFrames != null && velocityFrames.Length > 0)
        {
            // reset the frame arrays by reinstanitating
            velocityFrames = new Vector3[velocityFrames.Length];
            angularVelocity = new Vector3[angularVelocity.Length];
        }
    }

    Vector3 GetVectorAverage(Vector3[] frames)
    {

        Vector3 average = Vector3.zero;
        for (int i = 0; i < frames.Length; i++)
        {
            average+=frames[i];
        }

        average = average/frames.Length;

        return average;
    }

    void TimerCheck()
    {

        if (maxBounces != 0 && m_countedBounces >= maxBounces)
        {
            Reset();
        }
        else if (transform.position != StartPos && Vector3.Distance(StartPos, transform.position) > 1)
        {
            if (m_isStuck == true)
            {
                rb.isKinematic = true;
            }
            if (_throwTime == 0)
            {
                _throwTime = Time.realtimeSinceStartup + 100;
            }

            if (_throwTime < Time.realtimeSinceStartup)
            {
                if (transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                }

                _throwTime = 0;
                Reset();
            }
        }
        else if (_throwTime > 0)
            _throwTime = 0;

    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSound.transform.position = collision.contacts[0].point;
        hitSound.Play();

        if (collision.gameObject.CompareTag("Wall"))
        {
            collision.gameObject.GetComponent<WallSystem>().RunFunction(this);

            // Dogshit aimbot :)
            //float sped = rb.velocity.magnitude;
            //Vector3 dir = rb.velocity.normalized;

            //Vector3 enemyPos = Vector3.zero;

            //GameObject[] ar = GameObject.FindGameObjectsWithTag("NPC");
            //if (ar.Length < 1)
            //{
            //    GameObject e = GameObject.FindGameObjectWithTag("Goal");
            //    enemyPos = e.transform.GetComponent<Renderer>().bounds.center;
            //}
            //else
            //{
            //    float prevDistance = 1000;

            //    foreach (GameObject e in GameObject.FindGameObjectsWithTag("NPC"))
            //    {
            //        // HAHHAHAHHAH KILL ME PLEASE
            //        Vector3 ePos = e.transform.GetChild(0).GetComponent<Renderer>().bounds.center;

            //        float dist = Vector3.Distance(transform.position, ePos);

            //        if (dist < prevDistance)
            //        {
            //            prevDistance = dist;
            //            enemyPos = ePos;
            //        }
            //    }
            //}

            //Vector3 goal = (enemyPos - transform.position).normalized;
            //Vector3 lerp = Vector3.Lerp(dir, goal, 0.5f);
            ////lerp.y = dir.y;

            //rb.velocity = lerp * sped;
            //

        }
        if (collision.gameObject.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<NPC>().RunFunction(this);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            killSound.transform.position = other.gameObject.transform.position;
            killSound.Play();

            other.gameObject.GetComponent<NPC>().RunFunction(this);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<NPC>().RunFunction(this);
        }
    }
    

    public void Unstuck()
    {
        rb.isKinematic = false;
        m_isStuck = false;
    }

    // On Grab Enter -----
    public void UnlockConstraints()
    {
        rb.constraints = RigidbodyConstraints.None;
        spinmesh s = GetComponentInChildren<spinmesh>();
        rb.velocity = Vector3.zero;
        s.spinning = false;
        

    }


    // On Grab Exit -----
    public void LockConstraints()
    {

        AddVelocityHistory();
        ResetVelocityHistory();

       
        spinmesh s = GetComponentInChildren<spinmesh>();
        s.spinning = true;

        rb.constraints = RigidbodyConstraints.None;

        if (rotationConstraints.x == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
           
        }
        if (rotationConstraints.y == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
            
        }
        if (rotationConstraints.z == true)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
           
        }

        
    }
}
