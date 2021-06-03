using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public enum npcType
    {
        enemy,
        civilian,
        solider
    };

    [Header("NPC Type")]
    public npcType Type;

    // Update is called once per frame
    public void RunFunction(DiskController diskController)
    {
        switch (Type)
        {
            case npcType.enemy:
            {
                this.gameObject.SetActive(false);
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
