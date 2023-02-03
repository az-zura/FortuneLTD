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
    public float visibleRange = 50;

    public GameObject ghostNPCPrefabPedastrian;
    public GameObject ghostNPCPrefabRoad;
    private GameObject[] pointsOfInterest;
    private List<NPC_Goto> spawnPedestrian = new List<NPC_Goto>();
    private List<NPC_Goto> spawnRoadGhost = new List<NPC_Goto>();
    private int checkNPCIndex;

    private bool supsended = false;

    public void setSuspended(bool suspend)
    {
        this.supsended = supsended;
    }
    
    private List<GameObject> npcs = new List<GameObject>();
    
    public enum GhostType
    {
        pedastrian,
        roadGhost
    }

    private void Start()
    {
        GameObject.FindGameObjectsWithTag("NPC_Goto");
        pointsOfInterest = GameObject.FindGameObjectsWithTag("NPC_Goto");
        Debug.Log("found " + pointsOfInterest.Length +" points of interest");
        foreach (var p in pointsOfInterest)
        {
            NPC_Goto npcGoto = p.GetComponent<NPC_Goto>();
            if(!npcGoto)
            {
                Debug.LogError("Couldn't find NPC_Goto in object with NPC_Goto tag");
            }
            else
            {
                foreach (var spawnType in npcGoto.canSpawn)
                {
                    switch (spawnType)
                    {
                        case NPC_Goto.NPC_Spawn_Type.pedastrianGhost:
                            spawnPedestrian.Add(npcGoto);
                            break;
                        case NPC_Goto.NPC_Spawn_Type.roadGhost:
                            spawnRoadGhost.Add(npcGoto);
                            break;
                    }
                }
            }
            
        }

    }

    private void Update()
    {
        if (npcs.Count < amountNPCMin && !supsended)
        {
            for (int i = 0; i < Random.Range(amountNPCMin,amountNPCMax) - npcs.Count; i++)
            {
                if (Random.Range(0.0f, 1.0f) < 0.7)
                {
                    spawnNPC(GhostType.pedastrian);
                }
                else
                {
                    spawnNPC(GhostType.roadGhost);

                }
            }
        }

        //checks each tick if one actor is dispawnable 
        if (checkNPCIndex < npcs.Count)
        {
            checkNPC(npcs[checkNPCIndex]);
        }

        checkNPCIndex = (checkNPCIndex + 1) % (npcs.Count - 1);
    }

    private void checkNPC(GameObject ghost)
    {
        if (Vector3.Distance(player.transform.position, ghost.transform.position) > npcDespawnRange)
        {
            removeNPC(ghost);
        }
    }

    private Vector3[] getPath(List<NPC_Goto> possibleSpawns)
    {
        Vector3[] ret = new Vector3[2];
        int start = Random.Range(0, possibleSpawns.Count);
        ret[0] = possibleSpawns[start].transform.position;
        int end = Random.Range(start + 1, possibleSpawns.Count + start) % possibleSpawns.Count;
        ret[1] = possibleSpawns[end].transform.position;
        
        return ret;
    }

    private void spawnNPC(GhostType type)
    { 
        Vector3[] route;
        NPC_Locomotion ghostLocomotion;
        GameObject ghost;
        switch (type)
        { 
            case GhostType.pedastrian:
                route = getPath(this.spawnPedestrian);
                if (Vector3.Distance(route[0], player.transform.position) > npcDespawnRange) return;
                ghost = Instantiate(ghostNPCPrefabPedastrian, route[0], Quaternion.identity);
                ghostLocomotion = ghost.GetComponent<NPC_Locomotion>();
                npcs.Add(ghost);
                ghost.layer = LayerMask.NameToLayer("AiAgent");
                ghostLocomotion.MoveTo(route[1]);
                ghostLocomotion.PathEndReached += reachedPathEnd;
                break;
            
            case GhostType.roadGhost:
                route = getPath(this.spawnRoadGhost);
                if (Vector3.Distance(route[0], player.transform.position) < visibleRange) return;
                
                ghost = Instantiate(ghostNPCPrefabRoad, route[0], Quaternion.identity);
                ghostLocomotion = ghost.GetComponent<NPC_Locomotion>();
                npcs.Add(ghost);
                ghost.layer = LayerMask.NameToLayer("AiAgent");
                ghostLocomotion.MoveTo(route[1]);
                ghostLocomotion.PathEndReached += reachedPathEnd;
                break;
        }
    }

    private void reachedPathEnd(object sender, EventArgs e)
    {
        NPC_Locomotion l = sender as NPC_Locomotion;
        if (l)
        {
            removeNPC(l.gameObject);
        }
    }

    private void removeNPC(GameObject ghost)
    {
        npcs.Remove(ghost);
        Destroy(ghost);
    }



    //private void isInPlayerRange(GameObject npc)
    //{
    //    if (Vector3.Distance(player.transform.position, npc.transform.position) >= npcDespawnRange)
    //    {
    //        Destroy(npc);
    //    }
    //}
}
