using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class TestAction : ActionBase
{

    public string name;
    
    public TestAction(string name)
    {
        this.name = name;
    }

    public override void OnResumeExecution()
    {
        EndEventItem();
    }
    
    public override void OnItemStart()
    {
        Debug.Log("Ran test action " + name);
        SuspendAction(1);
    }


}
