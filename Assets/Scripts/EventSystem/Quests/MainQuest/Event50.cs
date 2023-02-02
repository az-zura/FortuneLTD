using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using UnityEngine;

public class Event50 : SequentialEvent
{
    public MainQuest mainQuest;
    public GameObject player;
    public Speechbubble bubble;

    public GameLoop gameLoop;
    public PlayerInTriggerBoxCondition playerInOffice;
    
    
    public NPCControllerEmployee karlNpcControllerEmployee;
    public Transform carlGoto;
    
    public override void OnEventInitialized()
    {
        AddEventItem(new TestAction("starting 50, wait for conditions to be met"));

        AddEventItem(new CyclicCondition(1).AddCondition(playerInOffice).AddCondition(new IsWorkingHourCondition(gameLoop)));
        //player is in office and during working hours
        AddEventItem(new SetEventStateAction(mainQuest,50));
        AddEventItem(new SetNpcControllerPossession(true,karlNpcControllerEmployee));
        AddEventItem(new NPCGotoAction(karlNpcControllerEmployee.GetLocomotion,carlGoto.position,0,0,false));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,"Joe JOE JOOEEEEEE!!!!"));
        AddEventItem(new NPCLookAtAction(karlNpcControllerEmployee.GetAnimation,player.transform));
        AddEventItem(new SpeakAction(bubble,player,"Was?"));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,"Die Abstimmung für den Mitarbeiter des Monats beginnt bald"));
        AddEventItem(new SpeakAction(bubble,player,"Ja und?"));
        AddEventItem(new SpeakAction(bubble,player,"Hat mich doch noch nie wirklich interessiert"));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
                "Nicht interessiert? Joe, das ist doch kein richtiges Leben."));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
                " Ein Geist sollte immer produktiv sein."));
        AddEventItem(new SpeakAction(bubble,player,"Ich weiß nicht, Karl. Den Titel bekommt doch jeder, der halbwegs seine Arbeit erledigt"));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "Ich weiß ja nicht, wie du mit der Einstellung so viele bekommen hast, aber ich hatte ihn bis jetzt noch nicht."));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "Doch das wird sich heute ändern."));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "Ich werde Büroklammern sammeln und ich werde sogar meine eigenen Flyer drucken."));
        AddEventItem(new SpeakAction(bubble, player,
            "Karl, mach das. Ich bin mir sicher, Karen wird begeistert von deinen Flyern sein."));
        AddEventItem(new SpeakAction(bubble, player,
            "Und während du dich aufs Gewinnen konzentrierst, mach ich meine Arbeit und gehe Mittagessen, das halte ich zum Beispiel auch für sehr wichtig."));
        
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "Ach Joe, es ist genau diese Verlierer-Mentalität, die dich dieses Mal den Titel kosten wird."));        
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            " Wir müssen uns selbst vermarkten, um in dieser Welt zu überleben. Dieses Mal werde ich Mitarbeiter des Monats"));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "Und deine Gehaltserhöhung..."));
        AddEventItem(new SpeakAction(bubble,karlNpcControllerEmployee.gameObject,
            "...Pustekuchen"));
        
        //cleanup
        AddEventItem(new NPCLookAtAction(karlNpcControllerEmployee.GetAnimation));
        AddEventItem(new SetNpcControllerPossession(false,karlNpcControllerEmployee));
        AddEventItem(new SetEventStateAction(mainQuest,60));

        
        StartSequentialEvent();
    }
}
