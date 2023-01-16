using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPC_Locomotion : MonoBehaviour
{
    private Vector3 currentDestination;
    private NavMeshAgent navMeshAgent;
    private CharacterController controller;
    private GhostAnimation ghostAnimation;
    private bool movedLastTick = false;

    private static int _maxPathEndIntents = 16; 
    //events
    public event EventHandler PathEndReached;
    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<CharacterController>();
        ghostAnimation = gameObject.GetComponentInChildren<GhostAnimation>();

    }

    // Update is called once per frame
    void Update()
    {
        if (movedLastTick != navMeshAgent.hasPath)
        {
            if (movedLastTick) OnPathEndReached();
            movedLastTick = navMeshAgent.hasPath;
            ghostAnimation.setMoving(movedLastTick);

        }
    }

    private NavMeshHit findStopOnNavMeshInRange(Vector3 center, float radiusMax, float radiusMin)
    {
        NavMeshHit hit;
        for (int i = 0; i < _maxPathEndIntents; i++)
        {
            Vector2 pos2D = Random.insideUnitCircle.normalized
                            * Random.Range(radiusMin,radiusMax);

            Vector3 samplePos = center + new Vector3(pos2D.x, 0, pos2D.y);
            NavMesh.SamplePosition(samplePos, out hit, 5, 1);
            if (hit.hit)
            {
                return hit;
            }
        }
        return new NavMeshHit();
    }

    public void OnPathEndReached()
    {
        EventHandler handler = PathEndReached;
        handler?.Invoke(this,EventArgs.Empty);
    }

    public bool moveTo(Vector3 destination, float minDistance = default, float maxDistance = default)
    {
        if (minDistance != default || maxDistance != default)
        {
            NavMeshHit hit = findStopOnNavMeshInRange(destination, minDistance, maxDistance);
            if (!hit.hit)
            {
                return false;
            }

            currentDestination = hit.position;
        }
        else
        {
            currentDestination = destination;
        }
        
        return navMeshAgent.SetDestination(currentDestination);
    }
}
