using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class FollowAction : ActionBase
{
    private NPCControllerFollow npcControllerFollow;
    private Transform transformToFollow;

    public FollowAction(NPCControllerFollow npcControllerFollow, Transform transformToFollow)
    {
        this.npcControllerFollow = npcControllerFollow;
        this.transformToFollow = transformToFollow;
    }

    public override void OnItemStart()
    {
        npcControllerFollow.transformToFollow = transformToFollow;
        npcControllerFollow.StartControllingNpc();
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
    }
}
