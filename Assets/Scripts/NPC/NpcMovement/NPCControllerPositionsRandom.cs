using System.Collections;
using System.Collections.Generic;
using NPC.NpcMovement;
using UnityEngine;

public class NPCControllerPositionsRandom : SpecificNpcController
{
    [SerializeField] private Transform[] positions;


    protected override void OnStartControlling()
    {
        Locomotion.MoveTo(positions[Random.Range(0,positions.Length)].position, 1, 1);
    }

    protected override void OnPathEnd()
    {
        StartCoroutine(waitAndMove());
    }

    protected override void OnStopControlling()
    {
        
    }

    IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(Random.Range(2f, 10f));
        if (IsControllingNpc) OnStartControlling();
    }
}
