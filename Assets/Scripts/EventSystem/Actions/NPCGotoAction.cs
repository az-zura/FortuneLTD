using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class NPCGotoAction : ActionBase
{
    private NPC_Locomotion locomotion;
    private Vector3 targetPosition;
    private float radiusMax;
    private float radiusMin;
    
    public NPCGotoAction(NPC_Locomotion locomotion, GameObject targetPosition, float radiusMin = default , float radiusMax = default)
    {
        this.locomotion = locomotion;
        this.targetPosition = targetPosition.transform.position;
        this.radiusMax = radiusMax;
        this.radiusMin = radiusMin;

    }

    public NPCGotoAction(NPC_Locomotion locomotion, Vector3 targetPosition, float radiusMin = default , float radiusMax = default)
    {
        this.locomotion = locomotion;
        this.targetPosition = targetPosition;
        this.radiusMax = radiusMax;
        this.radiusMin = radiusMin;
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }

    private void reached(object sender, EventArgs args)
    {
        EndAction();
    }

    public override void OnActionStart()
    {
        locomotion.moveTo(targetPosition,radiusMin,radiusMax);
        locomotion.PathEndReached += reached;
    }
}
