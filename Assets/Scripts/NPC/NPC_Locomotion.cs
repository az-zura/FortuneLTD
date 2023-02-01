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
    
    public enum NPCNavigationState
    {
        idle,
        walking,
        waiting
    }

    public NPCNavigationState navigationState = NPCNavigationState.idle;
    
    
    void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        controller = gameObject.GetComponent<CharacterController>();
        ghostAnimation = gameObject.GetComponentInChildren<GhostAnimation>();
        if (!navMeshAgent) Debug.LogError("navMeshAgent is not initialized");

    }

    public NavMeshAgent getNavmeshAgent()
    {
        return navMeshAgent;
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

    public void pauseNavigation()
    {
        setNPCMotionState(NPCNavigationState.waiting);
    }

    private NavMeshHit findStopOnNavMeshInRange(Vector3 center, float radiusMax, float radiusMin)
    {
        NavMeshHit hit;
        for (int i = 0; i < _maxPathEndIntents; i++)
        {
            Vector2 pos2D = Random.insideUnitCircle.normalized
                            * Random.Range(radiusMin,radiusMax);

            Vector3 samplePos = center + new Vector3(pos2D.x, 0, pos2D.y);
            NavMesh.SamplePosition(samplePos, out hit, 5, navMeshAgent.areaMask);
            if (hit.hit)
            {
                return hit;
            }
        }
        return new NavMeshHit();
    }

    public void OnPathEndReached()
    {
        Debug.Log("path end reached :"+gameObject.name);
        setNPCMotionState(NPCNavigationState.idle);
        EventHandler handler = PathEndReached;

        handler?.Invoke(this,EventArgs.Empty);
    }

    public void setNPCMotionState(NPCNavigationState state)
    {
        this.navigationState = state;
    }
    public bool MoveTo(Vector3 destination, float minDistance = default, float maxDistance = default)
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

        setNPCMotionState(NPCNavigationState.walking);
        if (!navMeshAgent) navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        return navMeshAgent.SetDestination(currentDestination);
    }

    public Vector3 getNavMeshDestination()
    {
        return navMeshAgent.destination;
    }
}
