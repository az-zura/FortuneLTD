using System;
using System.Collections;
using System.Collections.Generic;
using NPC.NpcMovement;
using UnityEngine;

public class NPCControllerFollow : SpecificNpcController
{

    public Transform transformToFollow;
    private void Update()
    {
        if (IsControllingNpc && Vector3.Distance(this.gameObject.transform.position, transformToFollow.position) > 3)
        {
            Locomotion.MoveTo(transformToFollow.position, 1, 2);
        }
    }

    protected override void OnStartControlling()
    {
    }

    protected override void OnPathEnd()
    {
    }
}
