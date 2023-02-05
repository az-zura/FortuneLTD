using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class HubertusAbschiedInit : SequentialEvent
{
    // Start is called before the first frame update
    public GameLoop gameLoop;
    public GameObject player;
    public Speechbubble bubble;
    public GameObject huberturs;
    public GameObject hubertusAbschiedsFeier2;
    public Transform lift;
    public PlayerInTriggerBoxCondition inOffice;
    
    public override void OnEventInitialized()
    {
        NPC_Locomotion hubertusLocomotion = huberturs.GetComponentInChildren<NPC_Locomotion>();
        GhostAnimation hubertusAnimation = huberturs.GetComponentInChildren<GhostAnimation>();
        AddEventItem(new TestAction("start hubertus"));
        AddEventItem(new CyclicCondition(1).AddCondition(new IsWorkingHourCondition(gameLoop)).AddCondition(inOffice));
        
        AddEventItem(new ToggleActiveAction(huberturs,true));
        AddEventItem(new TeleportAction(hubertusLocomotion,lift));

        AddEventItem(new NPCGotoAction(hubertusLocomotion,player,1,2));
        AddEventItem(new NPCLookAtAction(hubertusAnimation,player.transform));
        AddEventItem(new CyclicCondition(1).AddCondition(new PlayerCloseToCondition(player,huberturs,4)));

        AddEventItem(new SpeakAction(bubble,huberturs,"Hey Joe, kurze frage, hast du heute morgen so um 8 Zeit?"));
        AddEventItem(new SpeakAction(bubble,player,"Ahh du gehst auch ins Jenseits, oder?"));
        AddEventItem(new SpeakAction(bubble,huberturs,"Ja genau"));
        AddEventItem(new SpeakAction(bubble,player,"Herzlichen Glückwunsch!"));
        AddEventItem(new SpeakAction(bubble,huberturs,"Danke!"));
        AddEventItem(new SpeakAction(bubble,huberturs,"Ich würde heute Morgen nach der Arbeit Abschiedsfeier feiern"));
        AddEventItem(new SpeakAction(bubble,huberturs,"Ich gehe nähmlich dann heute schon"));
        AddEventItem(new SpeakAction(bubble,player,"Ach heute schon, dass ist ja Toll"));
        AddEventItem(new SpeakAction(bubble,player,"Klar, bin ich gerne dabei"));
        AddEventItem(new SpeakAction(bubble,huberturs,"Sehr schön, dann bis später"));
        AddEventItem(new ToggleActiveAction(hubertusAbschiedsFeier2,true));
        AddEventItem(new NPCLookAtAction(hubertusAnimation));
        AddEventItem(new NPCGotoAction(hubertusLocomotion,lift.position));
        AddEventItem(new ToggleActiveAction(huberturs,false));

        StartSequentialEvent();
    }
    
}
