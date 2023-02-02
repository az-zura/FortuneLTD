using System;
using System.Collections;
using System.Collections.Generic;
using NPC.NpcMovement;
using UnityEngine;

public class NPCControllerEmployee : SpecificNpcController
{

    public GameLoop gameLoop;
    public Transform elevator;
    public Transform desk;
    public Transform deskLookAt;

    private GameObject amature;
    
    private void Start()
    {
        gameLoop.WorkdayStarted += startWorkDay;
        gameLoop.WorkdayEnded += endWorkDay;
        amature = this.gameObject.transform.Find("Ghost/ghost").gameObject;
    }
    
    protected override void OnStartControlling()
    {
        if (gameLoop.IsWorkingTime())
        {
            startWorkDay(this,EventArgs.Empty);
        }
        else
        {
            endWorkDay(this,EventArgs.Empty);
        }
    }

    private void startWorkDay(object sender, EventArgs args)
    {
        if (!isControllingNpc) return;
        amature.SetActive(true);
        this.GetLocomotion.MoveTo(desk.position);
    }
    private void endWorkDay(object sender, EventArgs args)
    {
        if (!isControllingNpc) return;
        onLeafDesk();
        this.GetLocomotion.MoveTo(elevator.position);
    }

    private void onEnterDesk()
    {
        this.GetAnimation.startLookAt(deskLookAt.transform);
    }

    private void onLeafDesk()
    {
        this.GetAnimation.stopLookAt();
    }

        
    protected override void OnPathEnd()
    {
        if (!gameLoop.IsWorkingTime() && Vector3.Distance(gameObject.transform.position, elevator.position) < 2f)
        {
            amature.SetActive(false);
        } else if (Vector3.Distance(gameObject.transform.position, desk.position) < 1f)
        {
            onEnterDesk();
        }
    }

    protected override void OnStopControlling()
    {
        onLeafDesk();
    }
}
