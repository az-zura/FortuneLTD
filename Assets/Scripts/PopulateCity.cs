using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopulateCity : MonoBehaviour
{
    public GameObject player;
    public int amountNPCMin;
    public int amountNPCMax;
    public float npcDespawnRange = 50;

    public GameObject ghostNPCPrefabPedastrian;
    private GameObject[] pointsOfInterest;

    private List<GameObject> npcs = new List<GameObject>();
    
    public enum GhostType
    {
        pedastrian,
        roadGhost
    }

    private void Start()
    {
        pointsOfInterest = GameObject.FindGameObjectsWithTag("NPC_Goto");
        Debug.Log("found " + pointsOfInterest.Length +" points of interest");
    }

    private void Update()
    {
        if (npcs.Count < amountNPCMin)
        {
            for (int i = 0; i < Random.Range(amountNPCMin,amountNPCMax) - npcs.Count; i++)
            {
                spawnNPC(GhostType.pedastrian);
            }
        }
    }

    private Vector3[] getPath()
    {
        Vector3[] ret = new Vector3[2];
        int start = Random.Range(0, pointsOfInterest.Length);
        ret[0] = pointsOfInterest[start].transform.position;
        int end = Random.Range(start + 1, pointsOfInterest.Length + start) % pointsOfInterest.Length;
        ret[1] = pointsOfInterest[end].transform.position;
        
        return ret;
    }

    private void spawnNPC(GhostType type)
    {
        switch (type)
        {
            case GhostType.pedastrian:
                Vector3[] route = getPath();
                if (Vector3.Distance(route[0], player.transform.position) > npcDespawnRange) return;
                GameObject ghost = Instantiate(ghostNPCPrefabPedastrian, route[0], Quaternion.identity);
                NPC_Locomotion ghostLocomotion = ghost.GetComponent<NPC_Locomotion>();
                npcs.Add(ghost);
                ghost.layer = LayerMask.NameToLayer("AiAgent");
                ghostLocomotion.MoveTo(route[1]);
                ghostLocomotion.PathEndReached += reachedPathEnd;
                break;
        }
    }

    private void reachedPathEnd(object sender, EventArgs e)
    {
        Debug.Log("object " + sender + " reached path end");
        NPC_Locomotion l = sender as NPC_Locomotion;
        if (l)
        {
            npcs.Remove(l.gameObject);
            Destroy(l.gameObject);
            
        }
    }



    //private void isInPlayerRange(GameObject npc)
    //{
    //    if (Vector3.Distance(player.transform.position, npc.transform.position) >= npcDespawnRange)
    //    {
    //        Destroy(npc);
    //    }
    //}
}
