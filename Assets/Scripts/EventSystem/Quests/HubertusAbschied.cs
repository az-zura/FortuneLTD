using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class HubertusAbschied : SequentialEvent
{

    public Transform atendees;
    public GameLoop gameLoop;
    public GameObject player;
    public Speechbubble bubble;
    public GameObject parent;
    public PlayerNotInTriggerBoxCondition playerNotInTriggerBoxCondition;
    public GameObject huberturs;    public override void OnEventInitialized()
    {
        NPC_Locomotion hubertusLocomotion = huberturs.GetComponentInChildren<NPC_Locomotion>();
        GhostAnimation hubertusAnimation = huberturs.GetComponentInChildren<GhostAnimation>();

        AddEventItem(new CyclicCondition(1).AddCondition(new PlayerCloseToCondition(player,huberturs,10)));

        Debug.Log("start hubertus");
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Liebe Kollegen, nach 125 Jahren inder Fortune LTD,stehe ich heute vor euch allen, um mich von euch zu verabschieden.")); 
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Die mich schon länger kennen wissen, wie wichtig mir immer die Arbeit war, dennoch bin ich fest davon überzeugt, dass ihr das auch ohne mich hinbekommt."));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Aber eine letzte, sehr wichtige Sache noch."));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "In meinem Schreibtisch, in der untersten Schublade ganz hinten,"));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "liegen noch 53 Fälle, die dringend bearbeitet werden müssen."));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Es würde mich beruhigen zu wissen, dass sich jemand drum kümmert. Joe, kannst du das nicht machen?"));
        AddEventItem(new SpeakAction(bubble, player,
            "Klar doch"));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Ok, gut ich muss mit dir dann kurz noch die Einzelheiten über zwei der Fälle besprechen, die sind ein bisschen kniffliger!"));
        for (int i = 0; i < atendees.transform.childCount; i++)
        {
            var g = atendees.transform.GetChild(i).GetComponentInChildren<NPC_Locomotion>();
            AddEventItem(new TimerCondition(Random.Range(0.2f,0.8f)));
            AddEventItem(new NPCGotoAction(g,huberturs,15,30,false));
        }
        
        AddEventItem(new NPCLookAtAction(hubertusAnimation,player.transform));
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Joe, hör gut zu, ich hab gestern bei meinem letzten Gang durchs Büro die Pflanze in deinem Schrank gefunden."));
        
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Ich weis nicht, ob du weißt, was das bedeutet, aber mir bleibt nicht die Zeit, um das herauszufinden."));
        
        AddEventItem(new SpeakAction(bubble, huberturs,
            "Ein guter Freund von mir aber, kann dir da weiterhelfen, such ihn in der Weststraße..."));
        AddEventItem(new ToggleActiveAction(huberturs,false));
        AddEventItem(new CyclicCondition(2).AddCondition(playerNotInTriggerBoxCondition));
        AddEventItem(new ToggleActiveAction(parent,false));
        
        StartSequentialEvent();
    }
}
