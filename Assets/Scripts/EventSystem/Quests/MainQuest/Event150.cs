using System.Collections;
using System.Collections.Generic;
using EventSystem;
using EventSystem.Base;
using EventSystem.Conditions.CyclicWaitConditions;
using UnityEngine;

public class Event150 : SequentialEvent
{
    public GameLoop gameLoop;
    public MainQuest mainQuest;
    public Speechbubble bubble;
    public GameObject player;
    public NPCControllerPositionsRandom pam;
    
    public NPCControllerFollow hannahFollow;
    public NPCControllerPositionsRandom hannahRandom;

    public GameObject flowers1;

    public Transform livingRoomGoto;
    public Transform pamBedroomPos2;
    
    public HologramPath pathfinding;
    public Transform schoolGoTo;

    public override void OnEventInitialized()
    {
        GhostAnimation playerGhostAnimation = player.GetComponentInChildren<GhostAnimation>();
        AddEventItem(new TestAction("starting event 150 waiting for time condition"));
        AddEventItem(new CyclicCondition(1).AddCondition(new AfterDateCondition(17.5f,gameLoop)));
        AddEventItem(new TestAction("starting event 150, lets gooo"));
        AddEventItem(new SetEventStateAction(mainQuest,150));

        AddEventItem(new SetNpcControllerPossession(true, pam));
        AddEventItem(new SetNpcControllerPossession(true, hannahFollow));
        AddEventItem(new SetNpcControllerPossession(true, hannahRandom));
        AddEventItem(new NPCGotoAction(hannahFollow.GetLocomotion,livingRoomGoto.position,0,0,false));
        
        AddEventItem(new ToggleActiveAction(flowers1,true));
        AddEventItem(new NPCGotoAction(pam.GetLocomotion,pamBedroomPos2.position));
        AddEventItem(new NPCLookAtAction(pam.GetAnimation,flowers1.transform));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Joe, was ist das?"));
        AddEventItem(new SpeakAction(bubble,player,"Was ist was?"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Das da auf der Decke!"));
        AddEventItem(new EmotionAction(playerGhostAnimation,GhostAnimation.Emotion.Surprised));
        AddEventItem(new EmotionAction(playerGhostAnimation,GhostAnimation.Emotion.Default,2));
        AddEventItem(new SpeakAction(bubble,player,"Oh nein, geht da nicht zu nah ran!"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Es ist wunderschön..."));
        AddEventItem(new SpeakAction(bubble,player,"Pam, hör mir zu, das taucht seit gestern überall auf. Und gestern ist das einem Geist auf dem Weg zur Arbeit auch passiert und er wurde sofort von der Polizei mitgenommen!"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ich dachte immer, sowas gibt's nur im Jenseits und bei den Menschen..."));
        AddEventItem(new SpeakAction(bubble,player,"Ich mach das jetzt schnell weg, Hannah darf das nicht mitbekommen!"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Können wir es nicht so lassen? Nur bis heute Abend."));
        AddEventItem(new SpeakAction(bubble,player,"Bist du wahnsinnig? Wer weiß was das ist, die Polizei wird schon einen Grund haben, deswegen jemand wegzusperren."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Von draußen kann man es nicht sehen und uns kommt doch eh niemand besuchen."));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Ich bin nur noch 4 Tage hier, bitte Joe"));
        AddEventItem(new SpeakAction(bubble,player,"Na schön wie du willst, aber heute Abend wenn ich zurückkomme, werden wir es sofort wegmachen, versprochen?"));
        AddEventItem(new SpeakAction(bubble,pam.gameObject,"Versprochen!"));
        AddEventItem(new SpeakAction(bubble,player," Dann hol ich jetzt Hannah und wir fliegen sofort in die Schule."));
        
        AddEventItem(new NPCLookAtAction(hannahRandom.GetAnimation,player.transform));
        AddEventItem(new CyclicCondition(0.5f).AddCondition(new PlayerCloseToCondition(player, hannahRandom.gameObject,4f)));
        
        AddEventItem(new SpeakAction(bubble,hannahRandom.gameObject,"Warum das ganze rumgeschreie?"));
        AddEventItem(new SpeakAction(bubble,hannahRandom.gameObject,"Habt ihr gestritten?"));
        AddEventItem(new SpeakAction(bubble,player,"Ja, ich habe wieder den falschen Grauton zum Malen gekauft."));
        AddEventItem(new SpeakAction(bubble,player,"Wir müssen jetzt aber los, der Unterricht fängt heute früher an."));
        AddEventItem(new SpeakAction(bubble,hannahRandom.gameObject,"Und Frühstück?"));
        AddEventItem(new SpeakAction(bubble,player,"Heute nicht, der Toaster ist eh kaputt, komm!"));
        AddEventItem(new SpeakAction(bubble,hannahRandom.gameObject,"Ok"));
        
        AddEventItem(new ShowPathAction(pathfinding, schoolGoTo));
        
        AddEventItem(new NPCLookAtAction(hannahRandom.GetAnimation));
        AddEventItem(new FollowAction(hannahFollow,player.transform));
        AddEventItem(new SetEventStateAction(mainQuest,160));
        

        
        
        StartSequentialEvent();
    }
}