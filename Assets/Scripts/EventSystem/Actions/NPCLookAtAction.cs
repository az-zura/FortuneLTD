using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class NPCLookAtAction : ActionBase
{
    private GhostAnimation ghostAnimation;
    private Transform lookAt;

    public NPCLookAtAction(GhostAnimation ghostAnimation)
    {
        this.ghostAnimation = ghostAnimation;
        this.lookAt = null;
    }

    public NPCLookAtAction(GhostAnimation ghostAnimation, Transform looktAt)
    {
        this.ghostAnimation = ghostAnimation;
        this.lookAt = looktAt;
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }

    public override void OnItemStart()
    {
        if (this.lookAt)
        {
            ghostAnimation.startLookAt(this.lookAt);
        }
        else
        {
            ghostAnimation.stopLookAt();
        }
        EndEventItem();
    }
}
