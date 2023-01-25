using System.Collections;
using System.Collections.Generic;
using EventSystem.Base;
using NPC.NpcMovement;
using UnityEngine;

public class SetNpcControllerPossession : ActionBase
{

    private bool possess;
    private SpecificNpcController npcController;

    public SetNpcControllerPossession(bool possess, SpecificNpcController npcController)
    {
        this.possess = possess;
        this.npcController = npcController;
    }

    public override void OnItemStart()
    {
        if (possess)
        {
            npcController.StopControllingNpc();
        }
        else
        {
            npcController.StartControllingNpc();
        }
        EndEventItem();
    }

    public override void OnResumeExecution()
    {
        throw new System.NotImplementedException();
    }
}
