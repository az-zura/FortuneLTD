using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class TeleportAction : ActionBase
{
    private NPC_Locomotion objectToTeleportLocomotion;
    private Vector3 teleportTarget;

    public TeleportAction(NPC_Locomotion objectToTeleportLocomotion, Transform teleportTargetTransform)
    {
        this.objectToTeleportLocomotion = objectToTeleportLocomotion;
        this.teleportTarget = teleportTargetTransform.position;
    }

    public TeleportAction(NPC_Locomotion objectToTeleportLocomotion, Vector3 teleportTarget)
    {
        this.objectToTeleportLocomotion = objectToTeleportLocomotion;
        this.teleportTarget = teleportTarget;
    }

    public override void OnItemStart()
    {
        if (!objectToTeleportLocomotion.getNavmeshAgent().Warp(teleportTarget)) Debug.LogError("couldnt warp npc");
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
