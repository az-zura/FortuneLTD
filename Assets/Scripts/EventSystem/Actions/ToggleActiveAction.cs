using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class ToggleActiveAction : ActionBase
{

    private bool activate;
    private GameObject gameObject;

    public ToggleActiveAction(GameObject gameObject, bool activate)
    {
        this.activate = activate;
        this.gameObject = gameObject;
    }

    public override void OnItemStart()
    {
        gameObject.SetActive(activate);
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
