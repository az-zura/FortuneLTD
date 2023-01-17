using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UnityEngine;

public class NPCLookAtAction : ActionBase
{
    private GhostAnimation ghostAnimation;
    private Transform looktAt;

    public NPCLookAtAction(GhostAnimation ghostAnimation)
    {
        this.ghostAnimation = ghostAnimation;
        this.looktAt = null;
    }

    public NPCLookAtAction(GhostAnimation ghostAnimation, Transform looktAt)
    {
        this.ghostAnimation = ghostAnimation;
        this.looktAt = looktAt;
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }

    public override void OnActionStart()
    {
        ghostAnimation.lookAt = looktAt;
        EndAction();
    }
}
