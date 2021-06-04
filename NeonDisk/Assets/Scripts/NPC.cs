using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        var rb = body.GetComponent<Rigidbody>();
                        rb.useGravity = true;
                    }
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
