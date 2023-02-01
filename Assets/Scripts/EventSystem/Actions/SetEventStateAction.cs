using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using UnityEngine;

public class SetEventStateAction : ActionBase
{

    private MainQuest mainQuest;
    private int state;

    public SetEventStateAction(MainQuest mainQuest, int state)
    {
        this.mainQuest = mainQuest;
        this.state = state;
    }


    public override void OnItemStart()
    {
        mainQuest.setMainQuestState(state);
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
