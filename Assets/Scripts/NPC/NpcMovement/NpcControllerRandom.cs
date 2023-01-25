using System.Collections;
using System.Collections.Generic;
using NPC;
using NPC.NpcMovement;
using UnityEngine;

public class NpcControllerRandom : SpecificNpcController
{
    protected override void OnStartControlling()
    {
        Locomotion.MoveTo(this.transform.position, 5, 10);
    }

    protected override void OnPathEnd()
    {
        StartCoroutine(waitAndMove());
    }

    IEnumerator waitAndMove()
    {
        yield return new WaitForSeconds(Random.Range(2f, 10f));
        if (IsControllingNpc) OnStartControlling();
    }
}