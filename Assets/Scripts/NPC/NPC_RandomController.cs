using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_RandomController : MonoBehaviour
{

    public float speedMin;
    public float speedMax;

    public Mesh ghostMeshMale;
    public Mesh ghostMeshFemale;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        //set random speed
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Random.Range(speedMin, speedMax);

        //set random ghost gender 
        SkinnedMeshRenderer meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (Random.Range(0, 2) == 0)
        {
            meshRenderer.sharedMesh = ghostMeshMale;

        }
        else
        {
            meshRenderer.sharedMesh = ghostMeshFemale;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
