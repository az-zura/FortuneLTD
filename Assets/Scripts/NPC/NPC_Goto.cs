using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Goto : MonoBehaviour
{
    public enum NPC_Spawn_Type
    {
        pedastrianGhost,
        roadGhost
    }

    public NPC_Spawn_Type[] canSpawn;
    public bool isHidden;
    
}
