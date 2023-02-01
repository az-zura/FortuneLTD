using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class NPCGotoAction : ActionBase
{
    private NPC_Locomotion locomotion;
    private Vector3 targetPosition;
    private float radiusMax;
    private float radiusMin;
    private bool waitForTargetReached = true;
    
    public NPCGotoAction(NPC_Locomotion locomotion, GameObject targetPosition, float radiusMin = default , float radiusMax = default, bool waitForTargetReached = true)
    {
        this.locomotion = locomotion;
        this.targetPosition = targetPosition.transform.position;
        this.radiusMax = radiusMax;
        this.radiusMin = radiusMin;
        this.waitForTargetReached = waitForTargetReached;

    }

    public NPCGotoAction(NPC_Locomotion locomotion, Vector3 targetPosition, float radiusMin = default , float radiusMax = default , bool waitForTargetReached = true)
    {
        this.locomotion = locomotion;
        this.targetPosition = targetPosition;
        this.radiusMax = radiusMax;
        this.radiusMin = radiusMin;
        this.waitForTargetReached = waitForTargetReached;
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }

    private void reached(object sender, EventArgs args)
    {
        EndEventItem();
    }

    public override void OnItemStart()
    {
        locomotion.MoveTo(targetPosition,radiusMin,radiusMax);
        if (waitForTargetReached)
        {
            locomotion.PathEndReached += reached;
        }
        else
        {
            EndEventItem();
        }
        
    }
}
