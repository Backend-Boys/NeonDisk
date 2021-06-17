using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("NeonDisk/NPC Controller")]
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]

    public struct npcRigidBody
    {
        public bool breakable;
        public GameObject IntactModel;
        public GameObject DestructableModel;
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
    [ContextMenu("Get Body")]
    void GetBody()
    {
        if (Data.DestructableModel != null)
        {
            Data.body = Data.DestructableModel.GetComponentsInChildren<Rigidbody>();
        }
        else
        {
            Debug.LogError("DestructableModel is set to null, Please set destuctable model to get body");
        }
    }
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
                    Data.IntactModel.SetActive(false);
                    Data.DestructableModel.SetActive(true);
                    foreach(var body in Data.body)
                    {
                        body.isKinematic = false;
                        body.transform.parent = null;    
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
