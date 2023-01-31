using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class ShowOutlineAction : ActionBase
{
    private bool activate;
    private InteractableObject interactableObject;

    public ShowOutlineAction(InteractableObject interactableObject, bool activate)
    {
        this.activate = activate;
        this.interactableObject = interactableObject;
    }

    public override void OnItemStart()
    {
        interactableObject.showOutline = activate;
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
