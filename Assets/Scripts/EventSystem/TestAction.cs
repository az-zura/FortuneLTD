using System.Collections;
using System.Collections.Generic;
using EventSystem;
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
        EndAction();
    }
    
    public override void OnActionStart()
    {
        Debug.Log("Ran test action " + name);
        SuspendAction(1);
    }


}
