using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NeonDisk/NPC Controller")]
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]

    public struct npcRigidBody
    {
        public bool breakable;
        public Rigidbody[] body;
        [Header("NPC Type")]
        public npcType Type;
    }

    [System.Serializable]
    public enum npcType
    {
        enemy,
        civilian,
        solider
    };

    public npcRigidBody Data;
    // Update is called once per frame
    public void RunFunction(DiskController diskController)
    {
        switch (Data.Type)
        {
            case npcType.enemy:
            {

                if (Data.breakable)
                {
                    foreach(var body in Data.body)
                    {
                        
                        body.isKinematic = false;
                        body.transform.parent = null;
                        body.AddExplosionForce(0.5f, diskController.transform.position, 1);
                    }

                        Scoring.main.AddPoints();

                    Destroy(gameObject); 
                }
                break;
            }
            case npcType.civilian:
            {
                break;
            }
            case npcType.solider:
            {
                break;
            }
        } 
    }
}
