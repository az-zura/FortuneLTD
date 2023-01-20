using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_RandomController : MonoBehaviour
{

    public float speedMin;
    public float speedMax;
    
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        //set random speed
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = Random.Range(speedMin, speedMax);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
