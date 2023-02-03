using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;
using UnityEngine.UIElements;

public class Event90 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public Speechbubble bubble;

    public override void OnEventInitialized()
    {
        AddEventItem(new CyclicCondition(1).AddCondition(new AfterDateCondition(19,gameLoop)));
        
        
        StartSequentialEvent();
    }
    
}
