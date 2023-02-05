using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class ShowPathAction : ActionBase
{
    private HologramPath pathfinding;
    private Transform target;
    
    public ShowPathAction(HologramPath pathfinding, Transform target)
    {
        this.pathfinding = pathfinding;
        this.target = target;
    }
    
    public override void OnItemStart()
    {
        pathfinding.gameObject.SetActive(true);
        pathfinding.target = target;
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
